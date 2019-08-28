using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class WindBall : ModProjectile
	{public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wind Ball");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.alpha = 150;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		
	}
}

