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
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 1;
			Projectile.alpha = 255;
			AIType = ProjectileID.Bullet;
		}

	}
}

