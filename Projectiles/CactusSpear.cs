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
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = 19;
			Projectile.timeLeft = 90;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.ownerHitCheck = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 25;
		}
		
		public override void AI()
		{
			Main.player[Projectile.owner].direction = Projectile.direction;
			Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
			Main.player[Projectile.owner].itemTime = Main.player[Projectile.owner].itemAnimation;
            Vector2 dist = Vector2.Normalize(Projectile.velocity) * 40;
			Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2) + dist.X;
			Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2) + dist.Y;
			Projectile.position += Projectile.velocity * Projectile.ai[0];
            if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[0] = 3f;
				Projectile.netUpdate = true;
			}
			if (Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
			{
				Projectile.ai[0] -= 4.5f;
			}
			else
			{
				Projectile.ai[0] += 1.8f;
			}
			if (Main.player[Projectile.owner].itemAnimation == 0)
			{
				Projectile.Kill();
			}
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= 1.57f;
			}
		}
	}
}