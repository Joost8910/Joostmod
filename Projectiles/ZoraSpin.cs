using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class ZoraSpin : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zora Spin");
            Main.projFrames[projectile.type] = 6;
		}
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.timeLeft = 60;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 6;
            projectile.alpha = 120;
            drawHeldProjInFrontOfHeldItemAndArms = true;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.ai[0] == 0 && projectile.ai[1] == 0)
            {
                projectile.timeLeft = 40;
                projectile.idStaticNPCHitCooldown = 12;
            }
            if ((int)projectile.ai[1] % 12 == 0)
            {
                Main.PlaySound(42, player.Center, 205);
            }
            if (projectile.timeLeft < 20)
            {
                projectile.ai[1]++;
                projectile.velocity *= 0.9f;
                projectile.idStaticNPCHitCooldown = 12;
                projectile.alpha += 6;
                if ((int)projectile.ai[1] % 2 == 0)
                {
                    projectile.frame = (projectile.frame + 1) % 6;
                }
            }
            else if (projectile.timeLeft < 10)
            {
                projectile.idStaticNPCHitCooldown = 18;
                projectile.velocity *= 0.85f;
                projectile.ai[1]++;
                projectile.alpha += 6;
                if ((int)projectile.ai[1] % 3 == 0)
                {
                    projectile.frame = (projectile.frame + 1) % 6;
                }
            }
            else
            {
                projectile.ai[1] += 1 + projectile.ai[0];
                if ((int)projectile.ai[1] % 2 == 0)
                {
                    projectile.frame = (projectile.frame + 1) % 6;
                }
                if (!player.wet && projectile.velocity.Y < player.maxFallSpeed)
                {
                    projectile.velocity.Y += player.gravity;
                }
            }
            if (player.wet)
            {
                if (projectile.ai[0] == 0)
                {
                    if (projectile.ai[1] % 2 == 1)
                    {
                        projectile.ai[1]++;
                    }
                    projectile.ai[0] = 1;
                    projectile.timeLeft = 60;
                    projectile.idStaticNPCHitCooldown = 6;
                    projectile.alpha = 120;
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
                    projectile.velocity = ((home - 1f) * projectile.velocity + vel) / home;
            }
            player.velocity = projectile.velocity;
            player.fallStart = (int)(player.position.Y / 16f);
            player.ChangeDir(projectile.direction * (projectile.ai[1] % 12 < 6 ? projectile.direction : -projectile.direction));
            projectile.position = vector - projectile.Size / 2f - projectile.velocity;
            projectile.rotation = 0;
            projectile.spriteDirection = projectile.direction;
            player.heldProj = projectile.whoAmI;
            player.noItems = true;
            player.mount.Dismount(player);
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            player.fullRotationOrigin = player.Center - player.position;
            player.fullRotation = MathHelper.WrapAngle(projectile.rotation);
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (player.immuneTime < 6)
            {
                player.immune = true;
                player.immuneTime = 6;
            }
            if (target.knockBackResist > 0)
            {
                target.velocity = projectile.velocity;
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            player.fullRotation = 0;
            Main.PlaySound(19, player.Center, 1);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture("JoostMod/Projectiles/ZoraSpin_Glow");
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, rect, lightColor, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
        }
    }
}