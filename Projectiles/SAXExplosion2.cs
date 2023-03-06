using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SAXExplosion2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosion");
			Main.projFrames[Projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.aiStyle = 0;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 30;
			Projectile.tileCollide = false;
			Projectile.light = 0.95f;
			Projectile.ignoreWater = true;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 180);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void AI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			}
		}


	}
}
