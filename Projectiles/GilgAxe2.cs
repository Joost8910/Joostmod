using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GilgAxe2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh's Axe");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 52;
			Projectile.height = 52;
			Projectile.aiStyle = 3;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;

		}

	}
}
