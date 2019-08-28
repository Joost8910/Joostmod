using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Sand : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
			projectile.ignoreWater = false;
			projectile.tileCollide = false;
			projectile.alpha = 128;
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
