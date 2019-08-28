using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Pumpkin2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pumpkin");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 30;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.alpha = 255;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.ai[0] += 1f;
			if (projectile.ai[0] >= 60f)
			{
				projectile.velocity.Y = projectile.velocity.Y + 0.15f;
				projectile.velocity.X = projectile.velocity.X = 0.99f;
				
			}
		}
		public override void Kill(int timeLeft)
		{
			int target = 0;
			float max = 5000;
			for(int i = 0; i < 200; i++)
			{
				NPC n = Main.npc[i];
				if(n.active && !n.friendly && n.damage > 0 && !n.dontTakeDamage)
				{
					float dX = n.Center.X - projectile.Center.X; 
					float dY = n.Center.Y - projectile.Center.Y; 
					float distance = (float)Math.Sqrt((dX * dX + dY * dY));
				
					if(distance < max)
					{
						max = distance;
						target = n.whoAmI;
					}
				}
			}
            if (target == 0 && (Main.npc[target].friendly || Main.npc[target].dontTakeDamage))
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC n = Main.npc[i];
                    if (!n.active)
                    {
                        target = n.whoAmI;
                        break;
                    }
                }
            }
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6f, 0f, 321, projectile.damage, projectile.knockBack, projectile.owner, target, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6f, 0f, 321, projectile.damage, projectile.knockBack, projectile.owner, target, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, -5.2f, 321, projectile.damage, projectile.knockBack, projectile.owner, target, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, 5.2f, 321, projectile.damage, projectile.knockBack, projectile.owner, target, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, 5.2f, 321, projectile.damage, projectile.knockBack, projectile.owner, target, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, -5.2f, 321, projectile.damage, projectile.knockBack, projectile.owner, target, 0f);
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);				
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{

			
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}

			projectile.timeLeft -= 60;
			return false;
		}


	}
}
