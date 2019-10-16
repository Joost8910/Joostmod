using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BusterSword : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Buster Sword");
        }
        public override void SetDefaults()
        {
            projectile.width = 136;
            projectile.height = 120;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            target.velocity.Y -= knockback * target.knockBackResist * player.gravDir;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= projectile.knockBack * player.gravDir;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[0] == 0)
            {
                Player player = Main.player[projectile.owner];
                Vector2 unit = projectile.velocity;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * (int)(projectile.scale * 172), (int)(projectile.scale * 30), ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float speed = 1;
            bool channeling = !player.dead && !player.noItems && !player.CCed;
            if (!channeling)
            {
                projectile.Kill();
            }
            if (projectile.velocity.X < 0)
            {
                projectile.direction = -1;
            }
            else
            {
                projectile.direction = 1;
            }
            if (projectile.ai[0] == 0)
            {
                projectile.velocity.X = projectile.direction * 4;
                if (projectile.soundDelay >= 0)
                {
                    Main.PlaySound(42, projectile.Center, 220);
                    projectile.soundDelay = -1;
                }
                projectile.localAI[1] -= speed;
                if (projectile.localAI[1] >= -20)
                {
                    projectile.velocity.Y = (projectile.localAI[1] + 8) * player.gravDir * 2;
                }
                else
                {
                    projectile.Kill();
                }
                if (projectile.localAI[1] == -10)
                {
                    Vector2 vel = projectile.velocity;
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector13 = Main.MouseWorld - vector;
                        vector13.Normalize();
                        if (vector13.HasNaNs())
                        {
                            vector13 = Vector2.UnitX * (float)player.direction;
                        }
                        if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                        {
                            projectile.netUpdate = true;
                        }
                        vel = vector13;
                    }
                    Projectile.NewProjectile(player.Center, vel * 14f, mod.ProjectileType("BusterBeam"), projectile.damage, projectile.knockBack / 3, projectile.owner);
                }
            }
            projectile.velocity.Normalize();
            projectile.position = (vector - (projectile.Size / 2f)) + (projectile.velocity * projectile.width * 0.7f);
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 1.57f : 0) + 0.785f;
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.ai[0] > 0)
            {
                projectile.velocity.Normalize();
                float shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                for (int i = 0; i < projectile.frame; i++)
                {
                    Vector2 vel = projectile.velocity * (shootSpeed + (i * shootSpeed / 11));
                    Projectile.NewProjectile(vector, vel, (int)projectile.ai[1], projectile.damage, projectile.knockBack / 2f, projectile.owner);
                    Main.PlaySound(2, projectile.Center, 63);
                }
            }
        }
    }
}