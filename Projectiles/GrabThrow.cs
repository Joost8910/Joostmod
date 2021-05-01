using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrabThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impact");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.alpha = 75;
            projectile.light = 0.7f;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.aiStyle = -1;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (projectile.ai[0] >= 0 && Main.npc[(int)projectile.ai[0]].active)
            {
                NPC target = Main.npc[(int)projectile.ai[0]];
                projectile.scale = ((target.width + target.height) / 2f) / 20f;
                projectile.direction = target.velocity.X > 0 ? 1 : (target.velocity.X < 0 ? -1 : projectile.direction);
                projectile.position = target.Center - projectile.Size / 2;
                projectile.velocity = target.velocity / 2;
                if (target.velocity.X == 0 || target.velocity.Y == 0 || (projectile.timeLeft < 115 && Collision.SolidCollision(target.position - new Vector2(2, 2), target.width + 4, target.height + 4)))
                {
                    projectile.ai[0] = -1;
                    projectile.ai[1] = -1;
                    projectile.timeLeft = 2;
                    projectile.velocity = Vector2.Zero;
                }
            }
            else if (projectile.ai[1] >= 0 && Main.player[(int)projectile.ai[1]].active)
            {
                Player target = Main.player[(int)projectile.ai[1]];
                target.mount.Dismount(target);
                target.noItems = true;
                target.controlJump = false;
                target.controlDown = false;
                target.controlUp = false;
                target.controlLeft = false;
                target.controlRight = false;
                projectile.direction = target.velocity.X > 0 ? 1 : (target.velocity.X < 0 ? -1 : projectile.direction);
                projectile.position = target.Center - projectile.Size / 2;
                projectile.velocity = target.velocity / 2;
                if (target.velocity.X == 0 || target.velocity.Y == 0)
                {
                    projectile.ai[0] = -1;
                    projectile.ai[1] = -1;
                    projectile.timeLeft = 2;
                    projectile.velocity = Vector2.Zero;
                }
            }
            else
            {
                projectile.Kill();
            }
            projectile.rotation = projectile.velocity.ToRotation();
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (projectile.ai[0] >= 0)
            {
                NPC target = Main.npc[(int)projectile.ai[0]];
                hitbox = target.getRect();
            }
            else if (projectile.ai[1] >= 0)
            {
                Player target = Main.player[(int)projectile.ai[1]];
                hitbox = target.getRect(); 
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.whoAmI == (int)projectile.ai[0])
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool CanHitPvp(Player target)
        {
            if (target.whoAmI == (int)projectile.ai[1])
            {
                return false;
            }
            return base.CanHitPvp(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] >= 0)
            {
                NPC npc = Main.npc[(int)projectile.ai[0]];
                Main.player[projectile.owner].ApplyDamageToNPC(npc, damage / 2, 0, projectile.direction, false);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            hitDirection = projectile.direction;
            /*
            if (projectile.scale > 1)
            {
                damage = (int)(damage * projectile.scale);
            }
            */
            crit = true;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            /*
            if (projectile.scale > 1)
            {
                damage = (int)(damage * projectile.scale);
            }
            */
            crit = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.direction == -1)
            {
                effects = SpriteEffects.FlipVertically;
            }
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            }
            return false;
        }
    }
}

