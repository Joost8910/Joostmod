using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Leaf : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 400;
			//projectile.tileCollide = false;
		}
		Vector2 origin = new Vector2(0f, 0f);
		public override void AI()
		{
			if (projectile.timeLeft % 2 == 0)
			{
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 2, projectile.velocity.X/10, projectile.velocity.Y/10, 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
			 double deg = (double)projectile.ai[0];
			double rad = deg * (Math.PI / 180);
			double dist = 40; 
			Player P = Main.player[projectile.owner];
			if (projectile.ai[1] >= 1)
			{
				origin += projectile.velocity;
                projectile.netUpdate = true;
                projectile.ownerHitCheck = false;
			}
			else
			{
				origin = P.Center;
				projectile.ownerHitCheck = true;
			}
			projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
			projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;	
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			projectile.ai[0] += 10f;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 6;
			height = 6;
			fallThrough = true;
			return projectile.ai[1] >= 1;
		}
	}
}

