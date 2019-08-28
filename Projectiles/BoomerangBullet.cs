using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BoomerangBullet : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boomerang Bullet");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 180;
			projectile.alpha = 5;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.rand.Next(10) == 0)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BoomerangBullet2"), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);		
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, 39, projectile.velocity.X/2, projectile.velocity.Y/2, 0, default(Color), 0.75f);
				}
			}
		}
	}
}

