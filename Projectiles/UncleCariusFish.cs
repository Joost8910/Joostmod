using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class UncleCariusFish : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fish");
		}
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1000;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
            projectile.ignoreWater = true;
		}
        public override bool PreAI()
        {
            projectile.spriteDirection = projectile.direction;
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid < 200 && projectile.velocity.Y < 25)
            {
                projectile.velocity.Y += 0.08f;
            }
            return true;
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            float mult = 1f;
            if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid < 200)
            {
                mult = 0.75f;
            }
            Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 1);	
			projectile.timeLeft -= 100;
			if (projectile.velocity.X != oldVelocity.X)
			{
                projectile.velocity.X = -oldVelocity.X * mult;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * mult;
			}
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 33, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 33, projectile.velocity.X * -0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 33, projectile.velocity.X * 0.2f, projectile.velocity.Y * -0.2f, 100, default(Color), 1f);
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 33, projectile.velocity.X * -0.2f, projectile.velocity.Y * -0.2f, 100, default(Color), 1f);
			return false;
		}


	}
}

