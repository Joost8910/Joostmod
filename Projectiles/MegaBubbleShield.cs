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
	        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 72;
			Projectile.height = 72;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 64;
			Projectile.alpha = 100;
			Projectile.extraUpdates = 1;
		} 
		public override void AI()
		{
			Projectile.position.X = Main.player[Projectile.owner].MountedCenter.X - Projectile.width/2;
			Projectile.position.Y = Main.player[Projectile.owner].MountedCenter.Y - Projectile.height/2;
			Projectile.velocity.Y = 0;
			Projectile.rotation = 0;
		}

	}
}

