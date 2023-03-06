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
	        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 2400;
			Projectile.extraUpdates = 1;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[Projectile.owner];
			n.AddBuff(24, 180);
		}
		public override void AI()
		{
			if(Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80 && (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidType == 0 || Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidType == 2))
			{
				Projectile.Kill();
			}
			if (Projectile.timeLeft % 2 == 0)
			{
				int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127, Projectile.velocity.X/10, Projectile.velocity.Y/10, 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
			 double deg = (double)Projectile.ai[1];
			double rad = deg * (Math.PI / 180);
			double dist = Projectile.ai[1] / 60; 
			Projectile p = Main.projectile[(int)Projectile.ai[0]];
			Player P = Main.player[Projectile.owner];
			if (!p.active || p.timeLeft <= 0 || (p.type != Mod.Find<ModProjectile>("InfernalChakram").Type && p.type != Mod.Find<ModProjectile>("DousedChakram").Type))
			{
				Projectile.position.X = P.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width/2;
				Projectile.position.Y = P.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height/2;
			}
			else
			{
				Projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width/2;
				Projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height/2;				
			}
			Projectile.ai[1] += 5f;
			Projectile.rotation = Projectile.ai[1] * 0.0174f;
		}
	}
}

