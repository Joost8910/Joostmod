using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Richoshot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Richoshot");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 5;
			projectile.timeLeft = 300;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
			projectile.alpha = 50;
			projectile.extraUpdates = 1;
            projectile.scale = 1.5f;
			aiType = ProjectileID.Bullet;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
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

