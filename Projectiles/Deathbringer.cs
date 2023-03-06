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
			Projectile.width = 44;
			Projectile.height = 44;
			Projectile.aiStyle = 0;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 11;
            Projectile.tileCollide = false;
		}
		public override void AI()
        {
	         NPC host = Main.npc[(int)Projectile.ai[0]];
			Projectile.spriteDirection = host.spriteDirection;
			double rad = host.rotation + 1.57f; 
			Projectile.rotation = (float)rad + 0.78f;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation = (float)rad - 3.93f;
			}
            double dist = 44; 
            Projectile.position.X = host.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width/2;
            Projectile.position.Y = host.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height/2;
        }
	}
}
