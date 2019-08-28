using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class MythrilStaff2 : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twilight Staff");
		}
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 54;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = !player.noItems && !player.CCed && projectile.ai[1] < 30;
            if (channeling)
            {
                projectile.ai[1]++;
            }
            else
            {
                projectile.Kill();
            }
            projectile.position = vector - projectile.Size / 2f;
            projectile.direction = player.direction;
            projectile.ai[0] += projectile.direction;
            projectile.rotation = projectile.ai[0] * 0.0174f * 6.5f;
            projectile.timeLeft = 2;
            player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = MathHelper.WrapAngle(projectile.rotation);
            if (projectile.ai[1] % 3 == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -(float)Math.Cos(projectile.rotation + 0.785f) * 8, -(float)Math.Sin(projectile.rotation + 0.785f) * 8, mod.ProjectileType("BoltofLight"), projectile.damage, projectile.knockBack / 2, player.whoAmI);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)Math.Cos(projectile.rotation + 0.785f) * 8, (float)Math.Sin(projectile.rotation + 0.785f) * 8, mod.ProjectileType("BoltofNight"), projectile.damage, projectile.knockBack / 2, player.whoAmI);
            }

            return false;
        }

    }
}