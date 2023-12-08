using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class WaterWhip : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slapping Water Tendril");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.alpha = 100;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Vector2.Distance(Projectile.position, Projectile.oldPos[1]) > 20)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 33, -oldVelocity.X, -oldVelocity.Y);
                }
                SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.Center);
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.localAI[0] >= 4 && Vector2.Distance(Projectile.position, Projectile.oldPos[1]) > 5)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 start = Projectile.oldPos[1] + Projectile.Size / 2;
                Vector2 end = Projectile.Center;
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 32 * Projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            float mult = Vector2.Distance(Projectile.position, Projectile.oldPos[1]) * 0.05f;
            if (mult > 3f)
                mult = 3f;
            damage = (int)(damage * mult);
            knockback = 0;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            float mult = Vector2.Distance(Projectile.position, Projectile.oldPos[1]) * 0.05f;
            if (mult > 3)
                mult = 3f;
            damage = (int)(damage * mult);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.knockBackResist > 0)
            {
                Vector2 vel = Projectile.position - Projectile.oldPos[1];
                vel.Normalize();
                float mult = Vector2.Distance(Projectile.position, Projectile.oldPos[1]) * 0.05f;
                if (mult > 3f)
                    mult = 3f;
                target.velocity = vel * Projectile.knockBack * target.knockBackResist * mult;
            }
            SoundEngine.PlaySound(SoundID.Splash, Projectile.Center);
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 33, -target.velocity.X, -target.velocity.Y, 0, default, 2);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (!target.noKnockback)
            {
                Vector2 vel = Projectile.position - Projectile.oldPos[1];
                vel.Normalize();
                float mult = Vector2.Distance(Projectile.position, Projectile.oldPos[1]) * 0.05f;
                if (mult > 3)
                    mult = 3;
                target.velocity = vel * Projectile.knockBack * mult;
            }
            SoundEngine.PlaySound(SoundID.Splash, Projectile.Center);
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 33, -target.velocity.X, -target.velocity.Y, 0, default, 2);
            }
        }
        int nextProj = -1;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
            writer.Write(Projectile.localAI[1]);
            writer.Write(nextProj);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadSingle();
            Projectile.localAI[1] = reader.ReadSingle();
            nextProj = reader.ReadInt32();
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            int max = 30;
            Projectile.scale = 1f - Projectile.ai[0] / max;
            Projectile.width = (int)(32 * Projectile.scale);
            Projectile.height = (int)(32 * Projectile.scale);
            bool channeling = player.channel && !player.noItems && !player.CCed;
            int mana = 5;
            if (player.inventory[player.selectedItem].type == Mod.Find<ModItem>("WaterWhip").Type)
            {
                mana = player.inventory[player.selectedItem].mana / 2;
            }
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0]++;
            }
            if (channeling && player.CheckMana(mana))
            {
                if (Projectile.ai[1] == -1 && Projectile.localAI[0] % 40 == 0)
                {
                    player.CheckMana(mana, true);
                }
                Projectile.localAI[0]++;
                if (nextProj >= 0 && Main.projectile[nextProj].type != Projectile.type && Main.projectile[nextProj].owner != Projectile.owner)
                {
                    Projectile.localAI[0] = 5;
                }
                if (player.ownedProjectileCounts[Projectile.type] < max - 1 && Projectile.localAI[0] == 5)
                {
                    nextProj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0] + 1, Projectile.identity);
                    player.ownedProjectileCounts[Projectile.type]++;
                }
                if (Projectile.ai[1] != -1)
                {
                    if (Main.projectile[(int)Projectile.ai[1]].type == Projectile.type && Main.projectile[(int)Projectile.ai[1]].owner == Projectile.owner)
                    {
                        vector = Main.projectile[(int)Projectile.ai[1]].Center;
                    }
                    else
                    {
                        Projectile.Kill();
                    }
                }
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor = 20f * Projectile.scale;
                    /*
                    for (int i = 0; i < projectile.ai[0]; i++)
                    {
                        scaleFactor += 8f * (1f - (i / max));
                    }
                    */
                    Vector2 dir = Main.MouseWorld - vector;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * player.direction;
                    }
                    dir *= scaleFactor;
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    if (Projectile.ai[1] == -1)
                    {
                        Projectile.velocity = dir;
                    }
                    else
                    {
                        if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, Projectile.Center + dir, 1, 1))
                        {
                            Vector2 move = vector + dir - Projectile.Center;
                            if (move.Length() > 0)
                            {
                                move *= scaleFactor / move.Length();
                            }
                            float home = 5 + Projectile.ai[0] / 2;
                            Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                            if (Projectile.velocity.Length() < scaleFactor)
                            {
                                Projectile.velocity *= scaleFactor / Projectile.velocity.Length();
                            }
                        }
                        else
                        {
                            Projectile.velocity = Projectile.oldVelocity;
                        }
                    }
                }
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.position = vector - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            //projectile.spriteDirection = projectile.direction;
            Projectile.timeLeft = 2;
            if (Projectile.ai[0] == 0)
            {
                player.ChangeDir(Projectile.direction);
                player.itemTime = 10;
                player.itemAnimation = 10;
                player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height / Main.projFrames[Projectile.type] * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = lightColor;
            Main.EntitySpriteDraw(tex, drawPosition, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}