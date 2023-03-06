using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 2;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			AIType = ProjectileID.Shuriken;
		}
		
		public override void Kill(int timeLeft)
		{
			int shootNum = 16;
			float shootSpread = 360f;
			float spread = shootSpread * 0.0174f;
			float baseSpeed = (float)Math.Sqrt(Projectile.velocity.X/2 * Projectile.velocity.X/2 + Projectile.velocity.Y/2 * Projectile.velocity.Y/2);
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/shootNum;
			double deltaAngle = spread/shootNum;
			double offsetAngle;
			int i;
			for (i = 0; i < shootNum;i++ )
			{
				offsetAngle = startAngle + deltaAngle * i;
				Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), 22, Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
			SoundEngine.PlaySound(SoundID.Splash, Projectile.position);				
		}
	}
}

