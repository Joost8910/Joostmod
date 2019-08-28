using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SnowFlake : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snow Flake");
		}
		public override void SetDefaults()
		{
			projectile.width = 46;
			projectile.height = 46;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 300;
			projectile.alpha = 95;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 30;
			height = 30;
			return true;
		}
		public override void AI()
		{
            projectile.rotation = projectile.timeLeft * -projectile.direction;
			if (projectile.velocity.Y < 2.5f)
			{
				projectile.velocity.Y += 0.1f;
			}
			if (projectile.velocity.Y > 2.5f)
			{
				projectile.velocity.Y -= 0.2f;
			}
			projectile.ai[1]++;
			if(projectile.ai[1] >= 15)
			{
				if(Main.rand.Next(2) == 0)
				{
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 3f, 344, projectile.damage, 3, projectile.owner);
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, 0f, 344, projectile.damage, 3, projectile.owner);
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -3f, 344, projectile.damage, 3, projectile.owner);
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, 0f, 344, projectile.damage, 3, projectile.owner);
				}
				else
				{
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 2.2f, 2.2f, 344, projectile.damage, 3, projectile.owner);
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 2.2f, -2.2f, 344, projectile.damage, 3, projectile.owner);
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -2.2f, -2.2f, 344, projectile.damage, 3, projectile.owner);
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -2.2f, 2.2f, 344, projectile.damage, 3, projectile.owner);
				}
				projectile.ai[1] -= 15;
			}
		}
	}
}

