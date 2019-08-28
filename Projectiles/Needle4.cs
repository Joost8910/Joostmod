using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Needle4 : ModProjectile
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
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.alpha = 5;
			projectile.arrow = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}
		public override void AI()
		{
			projectile.velocity.Y += 0.3f;
			projectile.velocity.X *= 0.99f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.timeLeft -= 100;
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

