using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Deathbringer : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Death Knight");
		}
		public override void SetDefaults()
		{
			projectile.width = 44;
			projectile.height = 44;
			projectile.aiStyle = 0;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 11;
            projectile.tileCollide = false;
		}
		public override void AI()
        {
	         NPC host = Main.npc[(int)projectile.ai[0]];
			projectile.spriteDirection = host.spriteDirection;
			double rad = host.rotation + 1.57f; 
			projectile.rotation = (float)rad + 0.78f;
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation = (float)rad - 3.93f;
			}
            double dist = 44; 
            projectile.position.X = host.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
            projectile.position.Y = host.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;
        }
	}
}
