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
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 5;
			Projectile.arrow = true;
			AIType = ProjectileID.WoodenArrowFriendly;
		}
		public override void AI()
		{
			Projectile.velocity.Y += 0.3f;
			Projectile.velocity.X *= 0.99f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.timeLeft -= 100;
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}

			return false;
		}


	}
}

