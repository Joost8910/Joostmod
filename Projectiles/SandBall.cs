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
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.ignoreWater = false;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			if ((projectile.timeLeft % 5) == 0)
			{
int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 32, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);

Main.dust[num1].noGravity = true;
Main.dust[num1].velocity *= 0.1f;
			}
		}


	}
}
