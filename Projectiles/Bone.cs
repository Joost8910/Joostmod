using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Bone : ModProjectile
	{
		private bool spawn = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bone");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.tileCollide = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = ProjectileID.Bone;
		}
		public override bool PreAI()
		{
			if (!spawn)
			{
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BoneX"), projectile.damage * 2, projectile.knockBack * 2, projectile.owner);
					projectile.timeLeft = 1;
				}
				spawn = true;
			}
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.timeLeft -= 90;
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
	}
}


