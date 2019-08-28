using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GiantKnife : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant's Knife");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 38;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override void AI()
		{
			projectile.ai[0] += 1f;
			if (projectile.ai[0] >= 150f)
			{
				projectile.velocity.Y = projectile.velocity.Y + 0.15f;
				projectile.velocity.X = projectile.velocity.X = 0.99f;
				
			}

		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.timeLeft -= 100;
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X / 3;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y / 3;
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
			return false;
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(3, (int)projectile.position.X, (int)projectile.position.Y, 4);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-10, 11) * .25f, Main.rand.Next(-10, -5) * .25f, mod.ProjectileType("GiantKnife3"), (int)(projectile.damage * 1f), 0, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-10, 11) * .25f, Main.rand.Next(-10, -5) * .25f, mod.ProjectileType("GiantKnife2"), (int)(projectile.damage * 1f), 0, projectile.owner);
		}
	}
}

