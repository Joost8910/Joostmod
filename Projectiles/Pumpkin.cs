using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Pumpkin : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pumpkin");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 28;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.minion = true;
			//projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 1800;
			projectile.extraUpdates = 1;
		} 
Vector2 origin = new Vector2(0f, 0f);
		bool start = false;
		public void Initialize()
		{
			origin = Main.player[projectile.owner].Center;
			start = true;	
		}
		public override void AI()
		{
			if (!start)
			{
				Initialize();
			}
			if ((projectile.timeLeft % 10) == 0)
			{
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.01f;
			}
			 double deg = (double)projectile.ai[0];
			double rad = deg * (Math.PI / 180);
			double dist = 64; 
			Player P = Main.player[projectile.owner];
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			if (projectile.ai[1] >= 1)
			{
				origin += projectile.velocity;
                projectile.netUpdate = true;
                dist = 8;	
				projectile.rotation = (float)rad;
				projectile.ownerHitCheck = false;
			}
			else
			{
				origin = P.Center;
				projectile.ownerHitCheck = true;
			}
			projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
			projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;	
			projectile.ai[0] += 3.75f;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 8;
			height = 8;
			fallThrough = true;
			return projectile.ai[1] >= 1;
		}
	}
}

