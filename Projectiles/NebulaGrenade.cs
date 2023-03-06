using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class NebulaGrenade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nebula Grenade");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 26;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 5;
			Projectile.light = 0.5f;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.WoodenArrowFriendly;
		}

		public override void AI()
		{
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 150f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
				Projectile.velocity.X = Projectile.velocity.X = 0.99f;
				
			}

		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-10, 11) * .25f, Main.rand.Next(-10, -5) * .25f, 617, (int)(Projectile.damage * 1f), 0, Projectile.owner);
		}

	}
}


