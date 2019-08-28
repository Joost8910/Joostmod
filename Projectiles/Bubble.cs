using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Bubble : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 70;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 150;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if(player.HasMinionAttackTargetNPC)
			{
				NPC target = Main.npc[player.MinionAttackTargetNPC];
				float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
				float shootToY = target.position.Y - projectile.Center.Y;
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
				distance = 3f / distance;
				shootToX *= distance * 1.5f;
				shootToY *= distance * 1.5f;
				if (Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1))
				{
					projectile.velocity.X = shootToX;
					projectile.velocity.Y = shootToY;
				}
			}
		}
	}
}


