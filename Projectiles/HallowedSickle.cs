using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HallowedSickle : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Sickle");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 72;
			projectile.height = 72;
			projectile.aiStyle = 18;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 300;
			projectile.alpha = 55;
			projectile.extraUpdates = 1;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 24;
			height = 24;
			return true;
		}
	}
}
