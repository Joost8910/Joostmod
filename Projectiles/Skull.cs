using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Skull : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skull");
			Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 540;
			projectile.tileCollide = false;
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			for(int i = 0; i < 200; i++)
			{
				//Enemy NPC variable being set
				NPC target = Main.npc[i];

				//Getting the shooting trajectory
				float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
				float shootToY = target.position.Y - projectile.Center.Y;
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

				if(distance < 480f && !target.friendly && target.active && target.type != 488 && !target.dontTakeDamage)
				{
					distance = 3f / distance;
					shootToX *= distance * 2;
					shootToY *= distance * 2;
					projectile.velocity.X = shootToX;
					projectile.velocity.Y = shootToY;
					if(projectile.ai[1] == 60) 
					{
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, mod.ProjectileType("ShadowBolt"), projectile.damage, projectile.knockBack, projectile.owner); //Spawning a projectile
						Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 8);
						projectile.ai[1] = 0;
					}
				}
			}
			if (projectile.timeLeft >= 480)
			{
				projectile.ai[1]++;		
			}
			if (projectile.timeLeft == 480)
			{
				projectile.frame = 1;
			}
			if (projectile.timeLeft < 420)
			{
				projectile.frame = 2;
			}
			if (projectile.timeLeft >= 420)
			{		
				double deg = (double)projectile.ai[0];
				double rad = deg * (Math.PI / 180);
				double dist = 55; 
				Player P = Main.player[projectile.owner];
				projectile.position.X = P.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
				projectile.position.Y = P.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;	
				projectile.ai[0] += 9f;
			}
		}
	}
}

