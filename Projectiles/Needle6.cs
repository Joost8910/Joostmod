using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Needle6 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("1000 Needles");
		}
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1;
			projectile.alpha = 255;
			aiType = ProjectileID.Bullet;
		}

	}
}

