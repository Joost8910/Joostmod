using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HostileWind : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wind");
            Main.projFrames[projectile.type] = 12;
        }
		public override void SetDefaults()
		{
			projectile.width = 160;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.timeLeft = 200;
			projectile.alpha = 50;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
		}
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Y += 14;
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            height = 12;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.timeLeft > 170 || projectile.timeLeft < 20)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.timeLeft > 170 || projectile.timeLeft < 20)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            projectile.velocity.Y = 0;
            if (projectile.timeLeft > 195)
            {
                projectile.frame = 0;
                projectile.position -= projectile.velocity;
                projectile.spriteDirection = projectile.direction;
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

