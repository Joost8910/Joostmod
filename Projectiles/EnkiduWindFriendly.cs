using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EnkiduWindFriendly : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enkidu's Wind");
			Main.projFrames[Projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 28;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 150;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
		}
				public override void AI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			}
		}
	}
}

