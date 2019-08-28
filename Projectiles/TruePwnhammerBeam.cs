using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TruePwnhammerBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Pwnhammer");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 34;
			projectile.aiStyle = 27;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 90;
			projectile.alpha = 75;
			projectile.light = 0.5f;
			projectile.tileCollide = false;
			projectile.extraUpdates = 1;

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
         72, //Dust ID
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


	}
}

