using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Rock : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rock");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 14;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = ProjectileID.SpikyBall;
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.damage -= 30;
            projectile.knockBack *= 0.8f;
            if (projectile.damage <= 0)
            {
                projectile.Kill();
            }
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
		{
        	for (int i = 0; i < 4; i++)
			{
				int dustType = 1;
				int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-4, 4);
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-4, 4);
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
		}
	}
}


