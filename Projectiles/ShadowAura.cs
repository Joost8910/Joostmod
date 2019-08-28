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
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 5;
		}
		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 48;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 20;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 1;
			projectile.alpha = 100;
		} 
		public override void AI()
		{
			projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width/2;
			projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height/2;
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}
	}
}

