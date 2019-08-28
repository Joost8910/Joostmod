using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class InfernalChakram : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infernal Chakram");
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1800;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 12;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			if(Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 80 && (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 0 || Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 2))
			{
				int d = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X/2, projectile.velocity.Y/2, mod.ProjectileType("DousedChakram"), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
				for (int k = 0; k < 200; k++)
				{
					Projectile g = Main.projectile[k];
					if ((g.type == mod.ProjectileType("Fires") || g.type == mod.ProjectileType("Fires2")) && g.active && g.owner == projectile.owner)
					{
						g.ai[0] = d;
					}
				}
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 13);
				projectile.Kill();
			}
			projectile.rotation = projectile.timeLeft * 6;
			if (projectile.timeLeft >= 1799)
			{
				for (int l = 0; l < 200; l++)
				{
					Projectile f = Main.projectile[l];
					if ((f.type == mod.ProjectileType("Fires") || f.type == mod.ProjectileType("Fires2")) && f.active && f.owner == projectile.owner)
					{
						f.ai[0] = projectile.whoAmI;
					}
				}
			}
			if (projectile.timeLeft % 5 == 0)
			{
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, projectile.velocity.X/10, projectile.velocity.Y/10, 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
			if (projectile.timeLeft % 30 == 0)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Fires"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);	
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Fires2"), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);	
			}
			if (projectile.timeLeft <= 1760)
			{
				projectile.aiStyle = 3;
				projectile.tileCollide = false;
			}
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(24, 300);
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
			return false;
		}

	}
}

