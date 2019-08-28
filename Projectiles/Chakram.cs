using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Chakram : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chakram");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 1800;
			projectile.extraUpdates = 1;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
		Vector2 origin = new Vector2(0f, 0f);
		bool start = false;
		public void Initialize()
		{
			double rot = Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
			float x = projectile.Center.X - (int)(Math.Cos(rot) * -75);
			float y = projectile.Center.Y - (int)(Math.Sin(rot) * -75);
			origin = new Vector2(x, y);
			start = true;	
		}
		public override void AI()
		{
			if (!start)
			{
				Initialize();
			}
			origin += projectile.velocity;
			double deg = (double) projectile.ai[1]; 
			double rad = (deg * (Math.PI / 180)) + Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
			double dist = 75; 
		
			projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
			projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;
		
			projectile.ai[1] += 5f;
			projectile.rotation = projectile.ai[1];
		}
	}
}

