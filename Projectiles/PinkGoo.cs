using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class PinkGoo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pink Slime");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.thrown= true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.alpha = 50;
			aiType = ProjectileID.Shuriken;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.timeLeft -= 15;
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * 0.7f;
			}
			return false;
		}

	}
}
