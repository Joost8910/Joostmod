using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BitterEnd2 : ModProjectile
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
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 400;
			projectile.alpha = 150;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            projectile.rotation = projectile.timeLeft * projectile.velocity.Length() * 0.0174f;
        }
    }
}

