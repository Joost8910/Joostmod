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
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.aiStyle = 1;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 400;
			Projectile.alpha = 150;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.timeLeft * Projectile.velocity.Length() * 0.0174f;
        }
    }
}

