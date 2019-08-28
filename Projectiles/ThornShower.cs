using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ThornShower : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rose Weave");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 66;
			projectile.height = 66;
			projectile.aiStyle = 1;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 300;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.WoodenArrowFriendly;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 36;
			height = 36;
			return true;
		}
		public override void AI()
		{
            projectile.rotation = projectile.timeLeft * -projectile.direction;
			if (projectile.timeLeft % 30 == 0)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-5, 5) * .05f, -1f, mod.ProjectileType("Thorn"), (int)(projectile.damage * 0.75f), 0, projectile.owner);				
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
            projectile.velocity.X *= -1;
		    projectile.velocity.Y *= -1;
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
            return false;
        }
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * 1f, 33, (int)(projectile.damage * 1f), 7, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * -1f, 33, (int)(projectile.damage * 1f), 7, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-10, 11) * -1f, Main.rand.Next(-10, -5) * 1f, 33, (int)(projectile.damage * 1f), 7, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-10, 11) * -1f, Main.rand.Next(-10, -5) * -1f, 33, (int)(projectile.damage * 1f), 7, projectile.owner);	
		}

	}
}

