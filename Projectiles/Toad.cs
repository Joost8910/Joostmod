using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Toad : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toad");
			Main.projFrames[projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 13;
			projectile.aiStyle = 63;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			aiType = ProjectileID.BabySpider;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(20, 180);
		}
		public override void AI()
		{
			if (projectile.timeLeft >= 299)
            {
                projectile.localAI[0] = Main.rand.Next(8);
            }
            projectile.frame = (int)projectile.localAI[0];
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            if (projectile.localAI[0] >= 4)
            {
                projectile.localAI[0] -= 4;
            }
            else
            {
                projectile.localAI[0] += 4;
            }
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * 0.75f;
			}
			return false;
		}
	}
}
