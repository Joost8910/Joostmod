using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Fires : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infernal Chakram");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 2400;
			projectile.extraUpdates = 1;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(24, 180);
		}
		public override void AI()
		{
			if(Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 80 && (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 0 || Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 2))
			{
				projectile.Kill();
			}
			if (projectile.timeLeft % 2 == 0)
			{
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, projectile.velocity.X/10, projectile.velocity.Y/10, 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
			 double deg = (double)projectile.ai[1];
			double rad = deg * (Math.PI / 180);
			double dist = projectile.ai[1] / 60; 
			Projectile p = Main.projectile[(int)projectile.ai[0]];
			Player P = Main.player[projectile.owner];
			if (!p.active || p.timeLeft <= 0 || (p.type != mod.ProjectileType("InfernalChakram") && p.type != mod.ProjectileType("DousedChakram")))
			{
				projectile.position.X = P.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
				projectile.position.Y = P.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;
			}
			else
			{
				projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
				projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;				
			}
			projectile.ai[1] += 5f;
			projectile.rotation = projectile.ai[1] * 0.0174f;
		}
	}
}

