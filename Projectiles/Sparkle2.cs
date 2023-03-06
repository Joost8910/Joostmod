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
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 90;
			Projectile.light = 0.3f;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = false;
			Projectile.alpha = 128;
			AIType = ProjectileID.Bullet;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
		}
        public override void AI()
        {
            Projectile.velocity.X += Main.rand.NextFloat(-0.1f, 0.1f);
            Projectile.velocity.Y += Main.rand.NextFloat(-0.1f, 0.1f);
            Projectile.scale = Projectile.timeLeft / 90f;
        }

    }
}
