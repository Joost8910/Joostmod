using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Naginata : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Naginata");
		}
		public override void SetDefaults()
		{
			projectile.width = 58;
			projectile.height = 58;
			projectile.scale = 1f;
			projectile.aiStyle = 19;
			projectile.timeLeft = 20;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.ownerHitCheck = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.hide = true;
		}
		
		public override void AI()
		{
			Main.player[projectile.owner].direction = projectile.direction;
			Main.player[projectile.owner].heldProj = projectile.whoAmI;
			Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
			projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
			projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
			projectile.position += projectile.velocity * projectile.ai[0]; if (projectile.ai[0] == 0f)
			{
				projectile.ai[0] = 3f;
				projectile.netUpdate = true;
			}
			if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
			{
				projectile.ai[0] -= 1.5f;
			}
			else
			{
				projectile.ai[0] += 1.3f;
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