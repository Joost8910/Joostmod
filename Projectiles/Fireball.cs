using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Fireball : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 200;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.WoodenArrowFriendly;
		}
		public override void AI()
		{
			if(Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 80 && (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 0 || Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 2))
			{
				projectile.Kill();
			}
			projectile.rotation = projectile.timeLeft * 6;
			int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, projectile.velocity.X/10, projectile.velocity.Y/10, 100, default(Color), 1f);
			Main.dust[num1].noGravity = true;
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
		public override void Kill(int timeLeft)
		{
			int shootNum = 12;
			float shootSpread = 360f;
			float spread = shootSpread * 0.0174f;
			float baseSpeed = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
			double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y)- spread/shootNum;
			double deltaAngle = spread/shootNum;
			double offsetAngle;
			int i;
			for (i = 0; i < shootNum;i++ )
			{
				offsetAngle = startAngle + deltaAngle * i;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), mod.ProjectileType("Flamethrown"), projectile.damage, 0, projectile.owner);
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);				
		}
	}
}

