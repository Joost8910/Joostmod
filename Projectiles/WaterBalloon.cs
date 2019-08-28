using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class WaterBalloon : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Balloon");
		}
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = ProjectileID.Shuriken;
		}
		
		public override void Kill(int timeLeft)
		{
			int shootNum = 16;
			float shootSpread = 360f;
			float spread = shootSpread * 0.0174f;
			float baseSpeed = (float)Math.Sqrt(projectile.velocity.X/2 * projectile.velocity.X/2 + projectile.velocity.Y/2 * projectile.velocity.Y/2);
			double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y)- spread/shootNum;
			double deltaAngle = spread/shootNum;
			double offsetAngle;
			int i;
			for (i = 0; i < shootNum;i++ )
			{
				offsetAngle = startAngle + deltaAngle * i;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), 22, projectile.damage, projectile.knockBack, projectile.owner);
			}
			Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 0);				
		}
	}
}

