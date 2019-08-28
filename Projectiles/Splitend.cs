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
			projectile.width = 28;
			projectile.height = 28;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.alpha = 55;
			projectile.extraUpdates = 1;
		}
        public override void AI()
        {
            projectile.rotation += projectile.timeLeft * -projectile.direction * 0.0174f * 5;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

	}
}
