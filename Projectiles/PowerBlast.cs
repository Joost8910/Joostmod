using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class PowerBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit of Power");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 34;
			projectile.aiStyle = 27;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.timeLeft = 25;
			projectile.alpha = 225;
			projectile.tileCollide = false;
			projectile.extraUpdates = 1;

		}
		


	}
}

