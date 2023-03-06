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
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = 70;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 150;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if(player.HasMinionAttackTargetNPC)
			{
				NPC target = Main.npc[player.MinionAttackTargetNPC];
				float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
				float shootToY = target.position.Y - Projectile.Center.Y;
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
				distance = 3f / distance;
				shootToX *= distance * 1.5f;
				shootToY *= distance * 1.5f;
				if (Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
				{
					Projectile.velocity.X = shootToX;
					Projectile.velocity.Y = shootToY;
				}
			}
		}
	}
}


