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
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
            projectile.tileCollide = false;
			projectile.timeLeft = 600;
			projectile.alpha = 15;
			aiType = ProjectileID.Bullet;
		}
        public override void AI()
        {
            projectile.rotation = -projectile.timeLeft * 12 * 0.174f;
            if (projectile.timeLeft % 5 == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 32, 0, 4);
            }
            if (projectile.position.Y < 700)
            {
                projectile.Kill();
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

