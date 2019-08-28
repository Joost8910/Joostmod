using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Kerbal2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kerbal");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.width = 42;
			projectile.height = 42;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 180;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 15)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
			}
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


	}
}
