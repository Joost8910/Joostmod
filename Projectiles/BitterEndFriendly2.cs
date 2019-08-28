using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BitterEndFriendly2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bitter End");
		}
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.alpha = 150;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		
	}
}

