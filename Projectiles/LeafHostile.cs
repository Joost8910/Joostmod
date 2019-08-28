using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class LeafHostile : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf");
		}
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 400;
			projectile.tileCollide = false;
		}
		public override void AI()
		{
			if (projectile.timeLeft % 2 == 0)
			{
				int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 2, projectile.velocity.X/10, projectile.velocity.Y/10, 100, default(Color), 1f);
				Main.dust[num1].noGravity = true;
			}
			double deg = projectile.ai[1];
			double rad = deg * (Math.PI / 180);
            NPC host = Main.npc[(int)projectile.ai[0]];
            projectile.position.X = host.Center.X - (int)(Math.Cos(rad) * (400 - projectile.timeLeft)*1.2f) - projectile.width / 2;
            projectile.position.Y = host.Center.Y - (int)(Math.Sin(rad) * (400 - projectile.timeLeft)*1.2f) - projectile.height / 2;
            projectile.rotation = (float)rad;
            projectile.ai[1] += 2f / ((((401 - projectile.timeLeft) * 1.2f) * 3.14f) / 360);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
        }
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 12; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 2, projectile.velocity.X/10, projectile.velocity.Y/10, 100, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
    }
}

