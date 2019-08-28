using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GrogWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 100;
			projectile.tileCollide = false;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
		public override void AI()
        {
            if (projectile.localAI[0] < 10)
            {
                projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
                projectile.spriteDirection = projectile.direction;
                projectile.velocity = Vector2.Zero;
                projectile.localAI[0] = 10;
            }
            projectile.scale = projectile.timeLeft * 0.02f;
            projectile.position.X += projectile.scale * 16 * projectile.spriteDirection;
            if (Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.position.Y -= 16 * projectile.scale;
            }
            //int x = 8 + (int)(projectile.position.X/16)*16;
			//if (x != projectile.localAI[1])
			//{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 15f, mod.ProjectileType("GrogWave1"), projectile.damage, projectile.knockBack, projectile.owner, projectile.spriteDirection, projectile.scale);			
				//projectile.localAI[1] = x;
			//}
		}
	}
}
