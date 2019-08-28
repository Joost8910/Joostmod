using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class NebulaGrenade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nebula Grenade");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 26;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 5;
			projectile.light = 0.5f;
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

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-10, 11) * .25f, Main.rand.Next(-10, -5) * .25f, 617, (int)(projectile.damage * 1f), 0, projectile.owner);
		}

	}
}


