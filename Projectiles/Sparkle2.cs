using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Sparkle2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparkle");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 90;
			projectile.light = 0.3f;
			projectile.ignoreWater = false;
			projectile.tileCollide = false;
			projectile.alpha = 128;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
        public override void AI()
        {
            projectile.velocity.X += Main.rand.NextFloat(-0.1f, 0.1f);
            projectile.velocity.Y += Main.rand.NextFloat(-0.1f, 0.1f);
            projectile.scale = projectile.timeLeft / 90f;
        }

    }
}
