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
	        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 1800;
			Projectile.extraUpdates = 1;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
		}
		Vector2 origin = new Vector2(0f, 0f);
		bool start = false;
		public void Initialize()
		{
			double rot = Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
			float x = Projectile.Center.X - (int)(Math.Cos(rot) * -75);
			float y = Projectile.Center.Y - (int)(Math.Sin(rot) * -75);
			origin = new Vector2(x, y);
			start = true;	
		}
		public override void AI()
		{
			if (!start)
			{
				Initialize();
			}
			origin += Projectile.velocity;
			double deg = (double) Projectile.ai[1]; 
			double rad = (deg * (Math.PI / 180)) + Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
			double dist = 75; 
		
			Projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - Projectile.width/2;
			Projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - Projectile.height/2;
		
			Projectile.ai[1] += 5f;
			Projectile.rotation = Projectile.ai[1];
		}
	}
}

