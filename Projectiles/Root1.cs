using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Root1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Root");
			Main.projFrames[Projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 70;
			Projectile.aiStyle = 0;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 150;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
		}
        public override bool CanHitPlayer(Player target)
        {
            return Projectile.timeLeft <= 60 && Projectile.timeLeft > 10;
        }
        public override void AI()
		{
			if (Projectile.timeLeft > 60)
			{
				Projectile.frame = 0;
				Projectile.height = 16;
			}
			if (Projectile.timeLeft == 60)
			{
				Projectile.position.Y = Projectile.position.Y - 26;
                SoundEngine.PlaySound(SoundID.Item65, Projectile.position);
            }
			if (Projectile.timeLeft <= 60 && Projectile.timeLeft > 55)
			{
				Projectile.frame = 1;
				Projectile.height = 42;
			}
			if (Projectile.timeLeft == 55)
			{
				Projectile.position.Y = Projectile.position.Y - 27;
			}
			if (Projectile.timeLeft <= 55 && Projectile.timeLeft > 10)
			{
				Projectile.frame = 2;
				Projectile.height = 70;
			}
			if (Projectile.timeLeft == 10)
			{
				Projectile.position.Y = Projectile.position.Y + 27;
			}
			if (Projectile.timeLeft <= 10 && Projectile.timeLeft > 5)
			{
				Projectile.frame = 1;
				Projectile.height = 42;
			}
			if (Projectile.timeLeft == 5)
			{
				Projectile.position.Y = Projectile.position.Y + 26;		
			}
			if (Projectile.timeLeft <= 5)
			{
				Projectile.frame = 0;
				Projectile.height = 16;
			}
		}
	}
}
