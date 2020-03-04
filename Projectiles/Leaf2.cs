using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Leaf2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 750;
			//projectile.tileCollide = false;
		}
		public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.timeLeft >= 749)
            {
                projectile.localAI[0] = player.Center.X;
                projectile.localAI[1] = player.Center.Y;
                projectile.frame = Main.rand.Next(8);
            }
            if (projectile.timeLeft % 2 == 0)
			{
				Color color = new Color(255, 255, 255);
				if (projectile.frame == 0)
				{
					color = new Color(240, 170, 0);
				}
				if (projectile.frame == 1)
				{
					color = new Color(211, 115, 0);
				}
				if (projectile.frame == 2)
				{
					color = new Color(170, 80, 35);
				}
				if (projectile.frame == 3)
				{
					color = new Color(170, 40, 35);
				}
				if (projectile.frame == 4)
				{
					color = new Color(255, 0, 85);
				}
				if (projectile.frame == 5)
				{
					color = new Color(190, 0, 90);
				}
				if (projectile.frame == 6)
				{
					color = new Color(165, 35, 170);
				}
				if (projectile.frame == 7)
				{
					color = new Color(60, 35, 170);
				}
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 1, projectile.velocity.X/10, projectile.velocity.Y/10, 100, color, 1f);
				Main.dust[num1].noGravity = true;
			}
			double deg = (double)projectile.ai[0];
			double rad = deg * (Math.PI / 180);
			double dist = 55; 
			if (projectile.ai[1] >= 1)
            {
                projectile.localAI[0] += projectile.velocity.X;
                projectile.localAI[1] += projectile.velocity.Y;
                projectile.netUpdate = true;
                projectile.ownerHitCheck = false;	
			}
			else
            {
                projectile.localAI[0] = player.Center.X;
                projectile.localAI[1] = player.Center.Y;
                projectile.ownerHitCheck = true;
            }
            Vector2 origin = new Vector2(projectile.localAI[0], projectile.localAI[1]);
            projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
			projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;	
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			projectile.ai[0] += 10f;
            if (Collision.SolidCollision(new Vector2(projectile.localAI[0] - 5, projectile.localAI[1] - 5), 10, 10))
            {
                projectile.Kill();
            }
        }
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 6;
			height = 6;
			fallThrough = true;
            return false;
        }
	}
}

