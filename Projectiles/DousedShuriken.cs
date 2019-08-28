using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DousedShuriken : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellfire Shuriken");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 26;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = false;
			projectile.penetrate = 2;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Shuriken;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
	}
}


