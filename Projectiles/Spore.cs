using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Spore : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroom Spore");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 70;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
		}
		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 7)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
			projectile.rotation = 0;
			if ((projectile.timeLeft % 5) == 0)
			{
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 17, projectile.velocity.X/10, projectile.velocity.Y/10, 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
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


