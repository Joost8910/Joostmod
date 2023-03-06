using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Pumpkin2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pumpkin");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 30;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 60f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
				Projectile.velocity.X = Projectile.velocity.X = 0.99f;
				
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
					float dX = n.Center.X - Projectile.Center.X; 
					float dY = n.Center.Y - Projectile.Center.Y; 
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
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 6f, 0f, 321, Projectile.damage, Projectile.knockBack, Projectile.owner, target, 0f);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -6f, 0f, 321, Projectile.damage, Projectile.knockBack, Projectile.owner, target, 0f);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 3f, -5.2f, 321, Projectile.damage, Projectile.knockBack, Projectile.owner, target, 0f);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 3f, 5.2f, 321, Projectile.damage, Projectile.knockBack, Projectile.owner, target, 0f);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -3f, 5.2f, 321, Projectile.damage, Projectile.knockBack, Projectile.owner, target, 0f);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -3f, -5.2f, 321, Projectile.damage, Projectile.knockBack, Projectile.owner, target, 0f);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);				
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{

			
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}

			Projectile.timeLeft -= 60;
			return false;
		}


	}
}
