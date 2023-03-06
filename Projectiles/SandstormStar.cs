using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SandstormStar : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Weather Star - Sandstorm");
		}
		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
            Projectile.tileCollide = false;
			Projectile.timeLeft = 600;
			Projectile.alpha = 15;
			AIType = ProjectileID.Bullet;
		}
        public override void AI()
        {
            Projectile.rotation = -Projectile.timeLeft * 12 * 0.174f;
            if (Projectile.timeLeft % 5 == 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 32, 0, 4);
            }
            if (Projectile.position.Y < 700)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
		{
            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = (int)(3600f * (8f + Main.rand.NextFloat() * 16f));
            Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
            if (Main.netMode != 1)
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }
	}
}

