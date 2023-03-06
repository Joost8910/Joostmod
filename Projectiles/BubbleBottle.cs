using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BubbleBottle : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble Knife");
	        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 5;
			Projectile.light = 0.5f;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Shuriken;
		}

		public override void AI()
		{
				
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] >= 13f)
			{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * .15f, Main.rand.Next(-10, -5) * .05f, Mod.Find<ModProjectile>("BubbleThrown").Type, (int)(Projectile.damage * 0.75f), 7, Projectile.owner);
				Projectile.ai[1] -= 10f;				
			}
		}

	}
}


