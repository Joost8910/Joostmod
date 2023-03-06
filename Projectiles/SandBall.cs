using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SandBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand Ball");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.aiStyle = 1;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.ignoreWater = false;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			if ((Projectile.timeLeft % 5) == 0)
			{
int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 32, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f);

Main.dust[num1].noGravity = true;
Main.dust[num1].velocity *= 0.1f;
			}
		}


	}
}
