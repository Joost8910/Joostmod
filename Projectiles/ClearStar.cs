using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using Terraria.Graphics.Effects;

namespace JoostMod.Projectiles
{
	public class ClearStar : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Weather Star - Clear");
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
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 13);
            }
            if (projectile.position.Y < 700)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.rainTime = 0;
            Main.raining = false;
            Main.maxRaining = 0f;
            Main.StopSlimeRain(true);
            Sandstorm.Happening = false;
            Sandstorm.TimeLeft = 0;
            Sandstorm.IntendedSeverity = 0f;
            if (Main.netMode != 1)
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }

    }
}

