using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Balancerang : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Balancerang");
		}
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 40;
			projectile.alpha = 5;
			projectile.light = 0.5f;
			projectile.extraUpdates = 1;
		}
		public override void AI()
		{
			projectile.ai[0] += 1f;
			if (projectile.ai[0] >= 60f)
			{
				projectile.velocity.Y = projectile.velocity.Y + 0.15f;
				projectile.velocity.X = projectile.velocity.X = 0.99f;
				
			}
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
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
		
			return false;
		}

		public override void Kill(int timeLeft)
		{

			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * 1f, mod.ProjectileType("Balancerang2"), (int)(projectile.damage * 1f), 7, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * -1f, mod.ProjectileType("Balancerang3"), (int)(projectile.damage * 1f), 7, projectile.owner);
				
		}

	}
}

