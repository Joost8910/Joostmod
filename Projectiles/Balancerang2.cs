using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Balancerang2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Balancerang");
		}
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 600;
			projectile.alpha = 5;
			projectile.light = 0.1f;
			projectile.extraUpdates = 1;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}


	}
}

