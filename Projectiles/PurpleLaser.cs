using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class PurpleLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purple Laser");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
            projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 6;
			projectile.timeLeft = 600;
			projectile.alpha = 50;
			projectile.light = 0.2f;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
				projectile.timeLeft -= 60;
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

