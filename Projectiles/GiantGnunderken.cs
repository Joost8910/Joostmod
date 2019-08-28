using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Projectiles
{
	public class GiantGnunderken : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnunderson's Giant Shuriken");
		}
		public override void SetDefaults()
		{
			projectile.width = 104;
			projectile.height = 104;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.timeLeft = 300;
			aiType = ProjectileID.Bullet;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 34;
			height = 34;
			return true;
		}
		public override void AI()
		{
			projectile.spriteDirection = -projectile.direction;
			projectile.ai[1] -= 7f * projectile.spriteDirection;
			projectile.rotation = projectile.ai[1] * 0.0174f;
		}
	}
}


