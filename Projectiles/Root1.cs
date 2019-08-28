using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Root1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Root");
			Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 70;
			projectile.aiStyle = 0;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 150;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}
        public override bool CanHitPlayer(Player target)
        {
            return projectile.timeLeft <= 60 && projectile.timeLeft > 10;
        }
        public override void AI()
		{
			if (projectile.timeLeft > 60)
			{
				projectile.frame = 0;
				projectile.height = 16;
			}
			if (projectile.timeLeft == 60)
			{
				projectile.position.Y = projectile.position.Y - 26;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 65);
            }
			if (projectile.timeLeft <= 60 && projectile.timeLeft > 55)
			{
				projectile.frame = 1;
				projectile.height = 42;
			}
			if (projectile.timeLeft == 55)
			{
				projectile.position.Y = projectile.position.Y - 27;
			}
			if (projectile.timeLeft <= 55 && projectile.timeLeft > 10)
			{
				projectile.frame = 2;
				projectile.height = 70;
			}
			if (projectile.timeLeft == 10)
			{
				projectile.position.Y = projectile.position.Y + 27;
			}
			if (projectile.timeLeft <= 10 && projectile.timeLeft > 5)
			{
				projectile.frame = 1;
				projectile.height = 42;
			}
			if (projectile.timeLeft == 5)
			{
				projectile.position.Y = projectile.position.Y + 26;		
			}
			if (projectile.timeLeft <= 5)
			{
				projectile.frame = 0;
				projectile.height = 16;
			}
		}
	}
}
