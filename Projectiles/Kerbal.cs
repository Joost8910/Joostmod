using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Kerbal : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kerbal");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 38;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
		}
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), Mod.Find<ModProjectile>("Kerbal2").Type, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);				
		}
	}
}

