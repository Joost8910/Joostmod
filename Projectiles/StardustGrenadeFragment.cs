using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class StardustGrenadeFragment : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stardust Grenade");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 70;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.tileCollide = false;
			Projectile.penetrate = 5;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 1;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 8;

		}

	}
}


