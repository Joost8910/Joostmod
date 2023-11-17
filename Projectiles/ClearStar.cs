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
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 13);
            }
            if (Projectile.position.Y < 700)
            {
                Projectile.Kill();
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
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }

    }
}

