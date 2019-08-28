using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Kunai : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kunai");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.WoodenArrowFriendly;
		}



	}
}
