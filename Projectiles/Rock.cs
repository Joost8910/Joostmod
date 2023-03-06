using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Rock : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rock");
		}
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			AIType = ProjectileID.SpikyBall;
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.damage -= 30;
            Projectile.knockBack *= 0.8f;
            if (Projectile.damage <= 0)
            {
                Projectile.Kill();
            }
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
		{
        	for (int i = 0; i < 4; i++)
			{
				int dustType = 1;
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-4, 4);
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-4, 4);
			}
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}


