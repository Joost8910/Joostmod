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
	public class BubbleShield : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble Shield");
		}
		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 48;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 184;
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

