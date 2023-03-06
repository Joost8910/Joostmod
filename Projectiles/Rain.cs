using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Rain : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rain");
		}
		public override void SetDefaults()
		{
			Projectile.width = 2;
			Projectile.height = 40;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			AIType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			Projectile.rotation = 0;
		}
	}
}

