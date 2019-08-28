using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class RocFeather : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Roc Feather");
		}
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.tileCollide = true;
			aiType = ProjectileID.Bullet;
		}	
	}
}

