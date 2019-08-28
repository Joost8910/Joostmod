using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BubbleThrown : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 70;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;

		}

	}
}


