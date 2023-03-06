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
			Main.projFrames[Projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 13;
			Projectile.aiStyle = 63;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			AIType = ProjectileID.BabySpider;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[Projectile.owner];
			n.AddBuff(20, 180);
		}
		public override void AI()
		{
			if (Projectile.timeLeft >= 299)
            {
                Projectile.localAI[0] = Main.rand.Next(8);
            }
            Projectile.frame = (int)Projectile.localAI[0];
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            if (Projectile.localAI[0] >= 4)
            {
                Projectile.localAI[0] -= 4;
            }
            else
            {
                Projectile.localAI[0] += 4;
            }
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y * 0.75f;
			}
			return false;
		}
	}
}
