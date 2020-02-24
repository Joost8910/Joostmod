using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Boulder : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boulder");
		}
		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 500;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.melee = true;
			aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (projectile.velocity.Y < 20)
            {
                projectile.velocity.Y += 0.3f;
            }
            if (projectile.ai[0] > 5)
            {
                projectile.tileCollide = true;
            }
            else
            {
                projectile.ai[0]++;
            }
            projectile.rotation = projectile.timeLeft * projectile.direction * 0.0174f * projectile.velocity.Y * 0.1f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 207, 1, 0.1f);
            for (int d = 0; d < 20; d++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 1, 0, 0, 0, default, 2);
            }
        }
	}
}
