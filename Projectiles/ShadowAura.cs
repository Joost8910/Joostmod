using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ShadowAura : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Aura");
	        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			Main.projFrames[Projectile.type] = 5;
		}
		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 48;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 20;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
			Projectile.alpha = 100;
		} 
		public override void AI()
		{
			Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width/2;
			Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height/2;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}
		}
	}
}

