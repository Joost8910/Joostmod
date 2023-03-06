using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Pop : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grenade Fish");
			Main.projFrames[Projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.width = 92;
			Projectile.height = 92;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
		}

		public override void AI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			}
		}


	}
}
