using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SlimeStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weather Star - Slime");
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
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 4, 0, 4, 60, Color.Green);
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
            Main.StartSlimeRain(true);
        }
	}
}

