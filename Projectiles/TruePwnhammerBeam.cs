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
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.aiStyle = 27;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 90;
			Projectile.alpha = 75;
			Projectile.light = 0.5f;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;

		}
		public override void AI()
		{
			if ((Projectile.timeLeft % 5) == 0)
			{
				//say you wanted to add particles that stay mostly still to leave a trail behind a projectile
int num1 = Dust.NewDust(
         Projectile.position,
         Projectile.width,
         Projectile.height,
         72, //Dust ID
         Projectile.velocity.X,
         Projectile.velocity.Y,
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

