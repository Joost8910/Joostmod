using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrognakBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 180;
            Projectile.alpha = 75;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.damage = (int)(Projectile.damage * 0.8f);
            Projectile.knockBack *= 0.8f;
            if (Projectile.damage < 1)
            {
                Projectile.Kill();
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            hitDirection = target.Center.X < player.Center.X ? -1 : 1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = 1 / player.GetAttackSpeed(DamageClass.Melee);
            }
            if (Projectile.ai[1] == 0)
            {
                Projectile.timeLeft += (int)((1 + Projectile.extraUpdates) * 10 * player.GetAttackSpeed(DamageClass.Melee));
                Projectile.ai[1] += player.gravDir;
                Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            }
            int gravDir = Projectile.ai[1] < 0 ? -1 : 1;
            if (Projectile.timeLeft >= 180)
            {
                Projectile.localAI[0] = player.Center.X;
                Projectile.localAI[1] = player.Center.Y;
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 dir = Main.MouseWorld - player.Center;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = dir;
                }
            }
            else
            {
                Projectile.localNPCHitCooldown = (int)Math.Max((10f - Projectile.ai[0]), 1);
                Projectile.localAI[0] += Projectile.velocity.X * Projectile.ai[0] * 6 / (1 + Projectile.extraUpdates);
                Projectile.localAI[1] += Projectile.velocity.Y * Projectile.ai[0] * 6 / (1 + Projectile.extraUpdates);
                Projectile.ai[0] += 1f / 30f;
                if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    int hitDir = Projectile.position.Y < Projectile.oldPos[1].Y ? -1 : 1;
                    int type = Mod.Find<ModProjectile>("GrogWave1").Type;
                    if (hitDir == -1)
                    {
                        type = Mod.Find<ModProjectile>("GrogWaveFlipped1").Type;
                    }
                    if (Projectile.timeLeft <= 180 - (40 * player.GetAttackSpeed(DamageClass.Melee)))
                    {
                        Vector2 pos = Projectile.Center;
                        for (int i = 0; i < 10; i++)
                        {
                            Point tilePos = pos.ToTileCoordinates();
                            if (Main.tile[tilePos.X, tilePos.Y + 1].HasTile)
                            {
                                pos.Y -= 16 * hitDir;
                            }
                        }
                        Projectile.NewProjectile(pos.X, pos.Y, 0, 15 * hitDir, type, Projectile.damage, Projectile.knockBack * 2, Projectile.owner, Projectile.direction, Projectile.scale);
                        Projectile.Kill();
                    }
                    else if (Projectile.timeLeft > 180 - (20 * player.GetAttackSpeed(DamageClass.Melee)))
                    {
                        Projectile.timeLeft = 180 - (int)(20 * player.GetAttackSpeed(DamageClass.Melee));
                        SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
                        float rot = (Projectile.spriteDirection > 0 ? (Projectile.ai[1] * gravDir) : (-Projectile.ai[1] + 180)) * (float)Math.PI / 180;
                        Vector2 rPos = rot.ToRotationVector2();
                        Projectile.localAI[0] -= 140 * rPos.X;
                        Projectile.localAI[1] -= 140 * rPos.Y;
                        Projectile.ai[1] += 150;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            //Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0, 15 * gravDir, type, projectile.damage, projectile.knockback * 2, projectile.owner, projectile.direction, projectile.scale);
                            Vector2 dir = Main.MouseWorld - new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
                            dir.Normalize();
                            Projectile.velocity = dir * Projectile.velocity.Length();
                            Projectile.netUpdate = true;
                        }
                    }
                }
            }
            if (Projectile.timeLeft % 4 == 0)
            {
                int num1 = Dust.NewDust(
                            Projectile.position,
                            Projectile.width,
                            Projectile.height,
                            197,
                            Projectile.velocity.X,
                            Projectile.velocity.Y,
                            (Projectile.timeLeft < 75) ? 150 - (Projectile.timeLeft * 2) : 0,
                            new Color(0, 255, 0),
                            2f
                            );
                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            double deg = Projectile.spriteDirection > 0 ? (Projectile.ai[1] + 30 * gravDir) : (-Projectile.ai[1] + 150);
            double rad = deg * (Math.PI / 180);
            double dist = 70;
            Vector2 origin = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
            Projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.rotation = Projectile.spriteDirection > 0 ? (float)rad : (float)(rad + 3.14f);
            Projectile.ai[1] += Projectile.ai[0] * 18 / (1 + Projectile.extraUpdates) * gravDir;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 197, 0, 0, 230 - timeLeft * 2, new Color(0, 255, 0), 3f).noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Color color = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            if (Projectile.timeLeft < 75)
            {
                color *= Projectile.timeLeft / 75f;
            }
            float rot = Projectile.rotation;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                float scale = Projectile.scale * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
                spriteBatch.Draw(tex, drawPos, rect, color, Projectile.oldRot[k], drawOrigin, scale, effects, 0f);
            }

            spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rot, drawOrigin, Projectile.scale, effects, 0f);

            return false;
        }
    }
}

