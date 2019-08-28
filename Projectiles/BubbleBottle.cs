using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BubbleBottle : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble Knife");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 5;
			projectile.light = 0.5f;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Shuriken;
		}

		public override void AI()
		{
				
			projectile.ai[1] += 1f;
			if (projectile.ai[1] >= 13f)
			{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * .15f, Main.rand.Next(-10, -5) * .05f, mod.ProjectileType("BubbleThrown"), (int)(projectile.damage * 0.75f), 7, projectile.owner);
				projectile.ai[1] -= 10f;				
			}
		}

	}
}


