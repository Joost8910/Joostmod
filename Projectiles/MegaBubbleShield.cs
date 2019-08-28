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
	public class MegaBubbleShield : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mega Bubble Shield");
	        ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 72;
			projectile.height = 72;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 64;
			projectile.alpha = 100;
			projectile.extraUpdates = 1;
		} 
		public override void AI()
		{
			projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width/2;
			projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height/2;
			projectile.velocity.Y = 0;
			projectile.rotation = 0;
		}

	}
}

