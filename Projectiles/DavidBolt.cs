using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DavidBolt : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bolt of David");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.aiStyle = 27;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 25;
			projectile.extraUpdates = 1;
			projectile.light = 0.3f;

		}
		public override void AI()
		{
			if ((projectile.timeLeft % 5) == 0)
			{
				//say you wanted to add particles that stay mostly still to leave a trail behind a projectile
int num1 = Dust.NewDust(
         projectile.position,
         projectile.width,
         projectile.height,
         178, //Dust ID
         projectile.velocity.X,
         projectile.velocity.Y,
         100, //alpha goes from 0 to 255
         default(Color),
         1f
         );

Main.dust[num1].noGravity = true;
Main.dust[num1].velocity *= 0.1f;
            }
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
			projectile.timeLeft -= 100;
			return false;
		}

	}
}

