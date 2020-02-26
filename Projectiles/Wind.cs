using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Wind : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wind");
            Main.projFrames[projectile.type] = 12;
        }
		public override void SetDefaults()
		{
			projectile.width = 160;
			projectile.height = 50;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.timeLeft = 200;
			projectile.alpha = 150;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 15;
		}
        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            projectile.velocity.Y = 0;
            if (projectile.timeLeft > 195)
            {
                projectile.frame = 0;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 190)
            {
                projectile.frame = 1;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 25)
            {
                if (projectile.timeLeft % 5 == 0)
                {
                    projectile.frame = 2 + ((projectile.frame - 1) % 5);
                }
                if (projectile.timeLeft < 160)
                {
                    projectile.tileCollide = true;
                }
            }
            else if (projectile.timeLeft > 20)
            {
                projectile.frame = 7;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 15)
            {
                projectile.frame = 8;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 10)
            {
                projectile.frame = 9;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 5)
            {
                projectile.frame = 10;
                projectile.position -= projectile.velocity;
            }
            else
            {
                projectile.frame = 11;
                projectile.position -= projectile.velocity;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            if (projectile.timeLeft > 25)
                projectile.timeLeft = 25;
            return false;
        }
    }
}

