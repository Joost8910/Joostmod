using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Jumbow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jumbow");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 200;
            projectile.height = 200;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 20;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.ai[1] = 1;
            if (Main.myPlayer == projectile.owner)
            {
                if (!player.noItems && !player.CCed)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale * player.arrowDamage * (player.archery ? 1.2f : 1);
                        projectile.ai[1] = 40f / (float)player.inventory[player.selectedItem].useTime;
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

            }
            if (player.channel && !player.noItems && !player.CCed)
            {
                if (projectile.ai[0] < 1)
                {
                    projectile.ai[0] += projectile.ai[1] * 0.03f;
                }
                else if (projectile.localAI[0] == 0)
                {
                    projectile.localAI[0] = 1;
                    Main.PlaySound(2, projectile.Center, 39);
                }
                projectile.timeLeft = 11;
            }
            if (projectile.ai[0] < 0.5f || projectile.timeLeft < 10)
            {
                projectile.frame = 0;
            }
            else if (projectile.ai[0] < 1f)
            {
                projectile.frame = 1;
            }
            else
            {
                projectile.frame = 2;
            }
            if (!player.channel && projectile.timeLeft == 10)
            {
                Projectile.NewProjectile(projectile.Center, projectile.velocity * projectile.ai[0], mod.ProjectileType("GiantArrow"), (int)(projectile.damage * projectile.ai[0]), projectile.knockBack * projectile.ai[0], projectile.owner);
                Main.PlaySound(2, projectile.Center, 5);
            }

            Vector2 dir = projectile.velocity;
            dir.Normalize();
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f + (dir * -30);
            projectile.rotation = dir.ToRotation() + (projectile.direction < 0 ? 3.14f : 0);
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
    }
}