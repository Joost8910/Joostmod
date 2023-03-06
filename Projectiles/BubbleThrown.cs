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
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = 70;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;

		}

	}
}


