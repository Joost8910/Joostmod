using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace JoostMod.Projectiles
{
	class Grenade : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 180;
		}

        public override bool CanHitPlayer(Player target)
        {
            return base.CanHitPlayer(target) && projectile.timeLeft <= 3;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.timeLeft <= 3)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
			{
				projectile.velocity.X = oldVelocity.X * -0.6f;
			}
			if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
			{
				projectile.velocity.Y = oldVelocity.Y * -0.6f;
			}
			return false;
		}

		public override void AI()
		{
			if (projectile.timeLeft <= 3)
			{
				projectile.tileCollide = false;
				projectile.alpha = 255;
				projectile.position = projectile.Center;
				projectile.width = 128;
				projectile.height = 128;
				projectile.position.X = projectile.position.X - (projectile.width / 2);
				projectile.position.Y = projectile.position.Y - (projectile.height / 2);
				projectile.knockBack = 10;
			}
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 5f)
			{
				projectile.ai[0] = 10f;
				if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
				{
					projectile.velocity.X = projectile.velocity.X * 0.98f;
					if (projectile.velocity.X > -0.01f && projectile.velocity.X < 0.01f)
					{
						projectile.velocity.X = 0f;
						projectile.netUpdate = true;
					}
				}
				projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			}
			projectile.rotation += projectile.velocity.X * 0.1f;
			return;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, projectile.position);
			for (int i = 0; i < 20; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int i = 0; i < 40; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
		}
	}
}
