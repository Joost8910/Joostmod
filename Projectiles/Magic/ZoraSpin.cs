using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class ZoraSpin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zora Spin");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.scale = 1.5f;
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;
            Projectile.alpha = 120;
            DrawHeldProjInFrontOfHeldItemAndArms = true;
        }
        /*
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            Player player = Main.player[projectile.owner];
            width = player.width + 2;
            height = player.height + 2;
            fallThrough = player.controlDown;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            Main.PlaySound(19, projectile.Center, 1);
            return false;
        }
        */
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.ai[0] == 0 && Projectile.ai[1] == 0)
            {
                Projectile.timeLeft = 40;
                Projectile.localNPCHitCooldown = 12;
            }
            if ((int)Projectile.ai[1] % 12 == 0)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_ghastly_glaive_pierce_1"), Projectile.Center); // 205
            }
            if (Projectile.timeLeft < 20)
            {
                Projectile.ai[1]++;
                Projectile.velocity *= 0.95f;
                Projectile.localNPCHitCooldown = 12;
                Projectile.alpha += 6;
                if ((int)Projectile.ai[1] % 2 == 0)
                {
                    Projectile.frame = (Projectile.frame + 1) % 6;
                }
            }
            else if (Projectile.timeLeft < 10)
            {
                Projectile.localNPCHitCooldown = 18;
                Projectile.velocity *= 0.9f;
                Projectile.ai[1]++;
                Projectile.alpha += 6;
                if ((int)Projectile.ai[1] % 3 == 0)
                {
                    Projectile.frame = (Projectile.frame + 1) % 6;
                }
            }
            else
            {
                Projectile.ai[1] += 1 + Projectile.ai[0];
                if ((int)Projectile.ai[1] % 2 == 0)
                {
                    Projectile.frame = (Projectile.frame + 1) % 6;
                }
                if (!player.wet && Projectile.velocity.Y < player.maxFallSpeed)
                {
                    Projectile.velocity.Y += player.gravity;
                }
            }
            bool rain = Main.raining && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, new Vector2(Projectile.Center.X + (Main.screenPosition.Y - Projectile.Center.Y) / 14f * Main.windSpeedCurrent * 12f, Main.screenPosition.Y - 20), 1, 1) && Main.screenPosition.Y <= Main.worldSurface * 16.0;
            if (player.wet || rain)
            {
                if (Projectile.ai[0] == 0)
                {
                    if (Projectile.ai[1] % 2 == 1)
                    {
                        Projectile.ai[1]++;
                    }
                    Projectile.ai[0] = 1;
                    Projectile.timeLeft = 60;
                    Projectile.localNPCHitCooldown = 6;
                    Projectile.alpha = 120;
                }
                Vector2 vel = Vector2.Zero;
                float speed = 10;
                if (player.controlRight)
                {
                    vel.X += speed;
                }
                if (player.controlLeft)
                {
                    vel.X -= speed;
                }
                if (player.controlUp || player.controlJump)
                {
                    vel.Y -= player.gravDir * speed;
                }
                if (player.controlDown)
                {
                    vel.Y += player.gravDir * speed;
                }
                if (vel.X != 0 && vel.Y != 0)
                {
                    vel *= 0.707f;
                }
                if (vel.Length() > 10)
                {
                    vel *= 10 / vel.Length();
                }
                player.controlLeft = false;
                player.controlRight = false;
                player.controlJump = false;
                player.controlMount = false;
                player.jump = 0;
                float home = 30f;
                if (vel != Vector2.Zero)
                    Projectile.velocity = ((home - 1f) * Projectile.velocity + vel) / home;
            }
            if (Math.Abs(player.velocity.X) < 0.0001f && Math.Abs(Projectile.velocity.X) > 1)
            {
                Projectile.velocity.X *= -1;
                SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.Center);
            }
            if (Math.Abs(player.velocity.Y) < 0.0001f && Math.Abs(Projectile.velocity.Y) > 1)
            {
                Projectile.velocity.Y *= -1;
                SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.Center);
            }
            player.velocity = Projectile.velocity;
            player.fallStart = (int)(player.position.Y / 16f);
            player.ChangeDir(Projectile.direction * (Projectile.ai[1] % 12 < 6 ? Projectile.direction : -Projectile.direction));
            Projectile.position = vector - Projectile.Size / 2f - Projectile.velocity;
            Projectile.rotation = 0;
            Projectile.spriteDirection = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.noItems = true;
            player.mount.Dismount(player);
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
            player.fullRotationOrigin = player.Center - player.position;
            player.fullRotation = MathHelper.WrapAngle(Projectile.rotation);
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (player.immuneTime < 6)
            {
                player.immune = true;
                player.immuneTime = 6;
            }
            if (target.knockBackResist > 0)
            {
                target.velocity = Projectile.velocity;
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.fullRotation = 0;
            SoundEngine.PlaySound(SoundID.Splash, player.Center);
            for (int i = 0; i <= 20; i++)
            {
                Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 33, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1f + Main.rand.NextFloat());
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/ZoraSpin_Glow");
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height / Main.projFrames[Projectile.type] * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPosition, rect, lightColor * (1 - Projectile.alpha / 255f), Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            Main.EntitySpriteDraw(tex, drawPosition, rect, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}