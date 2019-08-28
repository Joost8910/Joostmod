using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Needle7 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Needle");
		}
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 200;
			projectile.alpha = 255;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.ai[0] += 1f;
			if (projectile.ai[0] >= 60f)
			{
				projectile.velocity.Y = projectile.velocity.Y + 0.15f;
				projectile.velocity.X = projectile.velocity.X = 0.99f;
				
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.timeLeft -= 50;
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
			
			return false;
		}


	}
}
