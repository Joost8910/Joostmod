using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class UncleCariusFish : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fish");
		}
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 1000;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
            Projectile.ignoreWater = true;
		}
        public override bool PreAI()
        {
            Projectile.spriteDirection = Projectile.direction;
            if (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount < 200 && Projectile.velocity.Y < 25)
            {
                Projectile.velocity.Y += 0.08f;
            }
            return true;
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            float mult = 1f;
            if (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount < 200)
            {
                mult = 0.75f;
            }
            SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.position);	
			Projectile.timeLeft -= 100;
			if (Projectile.velocity.X != oldVelocity.X)
			{
                Projectile.velocity.X = -oldVelocity.X * mult;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y * mult;
			}
			Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 33, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
			Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 33, Projectile.velocity.X * -0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
			Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 33, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * -0.2f, 100, default(Color), 1f);
			Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 33, Projectile.velocity.X * -0.2f, Projectile.velocity.Y * -0.2f, 100, default(Color), 1f);
			return false;
		}


	}
}

