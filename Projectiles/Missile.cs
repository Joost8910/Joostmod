using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Missile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Missile");
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (projectile.timeLeft > 550)
            {
                int dustIndex = Dust.NewDust(projectile.Center - projectile.velocity, 1, 1, 127, 0, 0, 0, default(Color), 2f);
                Main.dust[dustIndex].noGravity = true;
            }
            else
            {
                projectile.velocity.Y += 0.3f;
                if (projectile.velocity.Y > 10)
                {
                    projectile.velocity.Y = 10;
                }
            }
        }
        public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Explosion2"), projectile.damage, projectile.knockBack, projectile.owner);
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);				
		}
	}
}

