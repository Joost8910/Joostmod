using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Explosion : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosion");
			Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 184;
			projectile.height = 184;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
			projectile.tileCollide = false;
			projectile.light = 0.95f;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 6;
			}
		}
	}
}
