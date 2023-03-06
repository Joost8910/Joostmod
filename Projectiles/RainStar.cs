using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class RainStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weather Star - Rain");
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
            if (Projectile.timeLeft % 2 == 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 33, 0, 4);
            }
            if (Projectile.position.Y < 700)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.StopSlimeRain(true);
            int num = 86400;
            int num2 = num / 24;
            Main.rainTime = Main.rand.Next(num2 * 8, num);
            if (Main.rand.Next(3) == 0)
            {
                Main.rainTime += Main.rand.Next(0, num2);
            }
            if (Main.rand.Next(4) == 0)
            {
                Main.rainTime += Main.rand.Next(0, num2 * 2);
            }
            if (Main.rand.Next(5) == 0)
            {
                Main.rainTime += Main.rand.Next(0, num2 * 2);
            }
            if (Main.rand.Next(6) == 0)
            {
                Main.rainTime += Main.rand.Next(0, num2 * 3);
            }
            if (Main.rand.Next(7) == 0)
            {
                Main.rainTime += Main.rand.Next(0, num2 * 4);
            }
            if (Main.rand.Next(8) == 0)
            {
                Main.rainTime += Main.rand.Next(0, num2 * 5);
            }
            float num3 = 1f;
            if (Main.rand.Next(2) == 0)
            {
                num3 += 0.05f;
            }
            if (Main.rand.Next(3) == 0)
            {
                num3 += 0.1f;
            }
            if (Main.rand.Next(4) == 0)
            {
                num3 += 0.15f;
            }
            if (Main.rand.Next(5) == 0)
            {
                num3 += 0.2f;
            }
            Main.rainTime = (int)((float)Main.rainTime * num3);
            ChangeRain();
            Main.raining = true;
        }
        private static void ChangeRain()
        {
            if (Main.cloudBGActive >= 1f || (double)Main.numClouds > 150.0)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.maxRaining = (float)Main.rand.Next(20, 90) * 0.01f;
                    return;
                }
                Main.maxRaining = (float)Main.rand.Next(40, 90) * 0.01f;
                return;
            }
            else if ((double)Main.numClouds > 100.0)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.maxRaining = (float)Main.rand.Next(10, 70) * 0.01f;
                    return;
                }
                Main.maxRaining = (float)Main.rand.Next(20, 60) * 0.01f;
                return;
            }
            else
            {
                if (Main.rand.Next(3) == 0)
                {
                    Main.maxRaining = (float)Main.rand.Next(5, 40) * 0.01f;
                    return;
                }
                Main.maxRaining = (float)Main.rand.Next(5, 30) * 0.01f;
                return;
            }
        }
	}
}

