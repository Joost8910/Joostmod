using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class CactusSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Spear");
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 19;
			projectile.timeLeft = 90;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.ownerHitCheck = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 25;
		}
		
		public override void AI()
		{
			Main.player[projectile.owner].direction = projectile.direction;
			Main.player[projectile.owner].heldProj = projectile.whoAmI;
			Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
            Vector2 dist = Vector2.Normalize(projectile.velocity) * 40;
			projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2) + dist.X;
			projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2) + dist.Y;
			projectile.position += projectile.velocity * projectile.ai[0];
            if (projectile.ai[0] == 0f)
			{
				projectile.ai[0] = 3f;
				projectile.netUpdate = true;
			}
			if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
			{
				projectile.ai[0] -= 4.5f;
			}
			else
			{
				projectile.ai[0] += 1.8f;
			}
			if (Main.player[projectile.owner].itemAnimation == 0)
			{
				projectile.Kill();
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= 1.57f;
			}
		}
	}
}