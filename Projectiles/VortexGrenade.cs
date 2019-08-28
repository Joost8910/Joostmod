using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class VortexGrenade : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vortex Grenade");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 24;
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
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 dir = -projectile.velocity;
                dir.Normalize();
                dir *= 8;
                Vector2 perturbedSpeed = dir.RotatedByRandom(MathHelper.ToRadians(30));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, 616, projectile.damage, projectile.knockBack*1.5f, projectile.owner);
            }
        }

	}
}


