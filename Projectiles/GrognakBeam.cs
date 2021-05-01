using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrognakBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 10;
            projectile.timeLeft = 180;
            projectile.alpha = 75;
            projectile.tileCollide = false;
            projectile.extraUpdates = 3;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = (int)(projectile.damage * 0.8f);
            projectile.knockBack *= 0.8f;
            if (projectile.damage < 1)
            {
                projectile.Kill();
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            hitDirection = target.Center.X < player.Center.X ? -1 : 1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = 1 / player.meleeSpeed;
            }
            if (projectile.ai[1] == 0)
            {
                projectile.timeLeft += (int)((1 + projectile.extraUpdates) * 10 * player.meleeSpeed);
                projectile.ai[1] += player.gravDir;
                projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
            }
            int gravDir = projectile.ai[1] < 0 ? -1 : 1;
            if (projectile.timeLeft >= 180)
            {
                projectile.localAI[0] = player.Center.X;
                projectile.localAI[1] = player.Center.Y;
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 dir = Main.MouseWorld - player.Center;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = dir;
                }
            }
            else
            {
                projectile.localNPCHitCooldown = (int)Math.Max((10f - projectile.ai[0]), 1);
                projectile.localAI[0] += projectile.velocity.X * projectile.ai[0] * 6 / (1 + projectile.extraUpdates);
                projectile.localAI[1] += projectile.velocity.Y * projectile.ai[0] * 6 / (1 + projectile.extraUpdates);
                projectile.ai[0] += 1f / 30f;
                if (Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    int hitDir = projectile.position.Y < projectile.oldPos[1].Y ? -1 : 1;
                    int type = mod.ProjectileType("GrogWave1");
                    if (hitDir == -1)
                    {
                        type = mod.ProjectileType("GrogWaveFlipped1");
                    }
                    if (projectile.timeLeft <= 180 - (40 * player.meleeSpeed))
                    {
                        Vector2 pos = projectile.Center;
                        for (int i = 0; i < 10; i++)
                        {
                            Point tilePos = pos.ToTileCoordinates();
                            if (Main.tile[tilePos.X, tilePos.Y + 1].active())
                            {
                                pos.Y -= 16 * hitDir;
                            }
                        }
                        Projectile.NewProjectile(pos.X, pos.Y, 0, 15 * hitDir, type, projectile.damage, projectile.knockBack * 2, projectile.owner, projectile.direction, projectile.scale);
                        projectile.Kill();
                    }
                    else if (projectile.timeLeft > 180 - (20 * player.meleeSpeed))
                    {
                        projectile.timeLeft = 180 - (int)(20 * player.meleeSpeed);
                        Main.PlaySound(0, projectile.Center);
                        float rot = (projectile.spriteDirection > 0 ? (projectile.ai[1] * gravDir) : (-projectile.ai[1] + 180)) * (float)Math.PI / 180;
                        Vector2 rPos = rot.ToRotationVector2();
                        projectile.localAI[0] -= 140 * rPos.X;
                        projectile.localAI[1] -= 140 * rPos.Y;
                        projectile.ai[1] += 150;
                        if (Main.myPlayer == projectile.owner)
                        {
                            //Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0, 15 * gravDir, type, projectile.damage, projectile.knockBack * 2, projectile.owner, projectile.direction, projectile.scale);
                            Vector2 dir = Main.MouseWorld - new Vector2(projectile.localAI[0], projectile.localAI[1]);
                            dir.Normalize();
                            projectile.velocity = dir * projectile.velocity.Length();
                            projectile.netUpdate = true;
                        }
                    }
                }
            }
            if (projectile.timeLeft % 4 == 0)
            {
                int num1 = Dust.NewDust(
                            projectile.position,
                            projectile.width,
                            projectile.height,
                            197,
                            projectile.velocity.X,
                            projectile.velocity.Y,
                            (projectile.timeLeft < 75) ? 150 - (projectile.timeLeft * 2) : 0,
                            new Color(0, 255, 0),
                            2f
                            );
                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            double deg = projectile.spriteDirection > 0 ? (projectile.ai[1] + 30 * gravDir) : (-projectile.ai[1] + 150);
            double rad = deg * (Math.PI / 180);
            double dist = 70;
            Vector2 origin = new Vector2(projectile.localAI[0], projectile.localAI[1]);
            projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
            projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;
            projectile.rotation = projectile.spriteDirection > 0 ? (float)rad : (float)(rad + 3.14f);
            projectile.ai[1] += projectile.ai[0] * 18 / (1 + projectile.extraUpdates) * gravDir;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 197, 0, 0, 230 - timeLeft * 2, new Color(0, 255, 0), 3f).noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Color color = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            if (projectile.timeLeft < 75)
            {
                color *= projectile.timeLeft / 75f;
            }
            float rot = projectile.rotation;
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
            for (int k = projectile.oldPos.Length - 1; k > 0; k--)
            {
                float scale = projectile.scale * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(tex, drawPos, rect, color, projectile.oldRot[k], drawOrigin, scale, effects, 0f);
            }

            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rot, drawOrigin, projectile.scale, effects, 0f);

            return false;
        }
    }
}

