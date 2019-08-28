using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DousedChakram : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doused Chakram");
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 3;
			projectile.tileCollide = false;
            projectile.ignoreWater = false;
            projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1800;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 12;
			aiType = ProjectileID.Bullet;
		}

	}
}

