using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Splitend : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Split End");
		}
		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 28;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 55;
			Projectile.extraUpdates = 1;
		}
        public override void AI()
        {
            Projectile.rotation += Projectile.timeLeft * -Projectile.direction * 0.0174f * 5;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

	}
}
