using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ImpTail : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Imp Lord's Tail");
		}
		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 60;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 16;
			projectile.tileCollide = false;
		}
        public override void AI()
		{
            NPC host = Main.npc[(int)projectile.ai[0]];
            projectile.direction = -host.direction;
            projectile.spriteDirection = projectile.direction;
            projectile.position = host.Center - projectile.Size/2;
            projectile.velocity = host.velocity;
            projectile.rotation = (16-projectile.timeLeft) * (float)(Math.PI / 180) * 22.5f * projectile.direction;
			projectile.ai[1] += 2f * (projectile.timeLeft / 400);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (projectile.timeLeft > 12)
            {
                hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, 60, 30);
            }
            else if (projectile.timeLeft > 8)
            {
                if (projectile.spriteDirection < 0)
                {
                    hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, 30, 60);
                }
                else
                {
                    hitbox = new Rectangle((int)projectile.Center.X, (int)projectile.position.Y, 30, 60);
                }
            }
            else if (projectile.timeLeft > 4)
            {
                hitbox = new Rectangle((int)projectile.position.X, (int)projectile.Center.Y, 60, 30);
            }
            else
            {
                if (projectile.spriteDirection > 0)
                {
                    hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, 30, 60);
                }
                else
                {
                    hitbox = new Rectangle((int)projectile.Center.X, (int)projectile.position.Y, 30, 60);
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (!target.HasBuff(BuffID.Venom))
            {
                target.AddBuff(BuffID.Venom, 180);
            }
        }
    }
}

