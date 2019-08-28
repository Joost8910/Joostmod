using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class TomatoHead : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("TomatoHead");
            Main.projFrames[projectile.type] = 3;
		}
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 20;
        }
        public override bool? CanHitNPC(NPC target)
		{
			return !target.friendly && projectile.ai[1] >= 20;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (player.immuneTime < projectile.localNPCHitCooldown)
            {
                player.immune = true;
                player.immuneTime = projectile.localNPCHitCooldown;
            }
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(40f * projectile.scale);
            hitbox.Height = (int)(40f * projectile.scale);
            hitbox.X -= (int)(((40f * projectile.scale) - 40) * 0.5f);
            hitbox.Y -= (int)(((40f * projectile.scale) - 40) * 0.5f);
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.localAI[1] = 1;
            if (Main.myPlayer == projectile.owner)
            {
                bool channeling = player.channel && !player.noItems && !player.CCed;
                if(channeling)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        projectile.scale = player.inventory[player.selectedItem].scale;
                        projectile.localAI[1] = (30f / (float)player.inventory[player.selectedItem].useTime) / player.meleeSpeed;
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector13;
                }
                else
                {
                    projectile.Kill();
                }
                if (!channeling)
                {
                    projectile.Kill();
                }
            }
            if (projectile.ai[1] == 0)
            {
                projectile.ai[0] = 0;
            }
            if (player.velocity.Y == 0)
            {
                projectile.ai[0] = 1;
            }
            float speed = 8.5f;
            float accel = (speed * projectile.localAI[1]) / 30f;
            projectile.localNPCHitCooldown = (int)(20f / projectile.localAI[1]);
            projectile.ai[1] = projectile.ai[1] < 30 ? projectile.ai[1]+ projectile.localAI[1] : 0;
            bool halt = ((player.controlLeft && player.velocity.X > 0 && projectile.velocity.X > 0) || (player.controlRight && player.velocity.X < 0 && projectile.velocity.X < 0));
            bool zip = ((player.velocity.X > 0 && projectile.velocity.X > 0 && player.controlRight) || (player.velocity.X < 0 && projectile.velocity.X < 0 && player.controlLeft));
			if (projectile.ai[1] < 10)
            {
                projectile.frame = 0;
                if (!zip)
                {
                    player.velocity.X *= 0.9f;
                }
            }
            else if (projectile.ai[1] < 20)
            {
                projectile.frame = 1;
                if (!zip)
                {
                    player.velocity.X *= 0.9f;
                }
            }
            else
            {
                if (projectile.soundDelay <= 0)
                {
                    projectile.soundDelay = (int)(15f / projectile.localAI[1]);
                    Main.PlaySound(0, (int)projectile.Center.X, (int)projectile.Center.Y, -1, 0.275f * projectile.scale, 0.3f - (0.5f * (projectile.scale - 1)));
                }
                projectile.frame = 2;
                player.velocity.Y = ((projectile.ai[0] > 0 && player.velocity.Y > -speed) || projectile.velocity.Y > 0) ? player.velocity.Y + projectile.velocity.Y * accel : player.velocity.Y;
                player.velocity.X = Math.Abs(player.velocity.X) < speed ? player.velocity.X + projectile.velocity.X * accel : player.velocity.X;
            }
            if (halt)
            {
                player.velocity.X *= 0.9f;
            }
            projectile.position = (projectile.velocity + vector) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
    }
}