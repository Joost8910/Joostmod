using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class ImpTail : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imp Lord's Tail");
        }
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 16;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Projectile.direction = -host.direction;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.position = host.Center - Projectile.Size / 2;
            Projectile.velocity = host.velocity;
            Projectile.rotation = (16 - Projectile.timeLeft) * (float)(Math.PI / 180) * 22.5f * Projectile.direction;
            Projectile.ai[1] += 2f * (Projectile.timeLeft / 400);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (Projectile.timeLeft > 12)
            {
                hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, 60, 30);
            }
            else if (Projectile.timeLeft > 8)
            {
                if (Projectile.spriteDirection < 0)
                {
                    hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, 30, 60);
                }
                else
                {
                    hitbox = new Rectangle((int)Projectile.Center.X, (int)Projectile.position.Y, 30, 60);
                }
            }
            else if (Projectile.timeLeft > 4)
            {
                hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.Center.Y, 60, 30);
            }
            else
            {
                if (Projectile.spriteDirection > 0)
                {
                    hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, 30, 60);
                }
                else
                {
                    hitbox = new Rectangle((int)Projectile.Center.X, (int)Projectile.position.Y, 30, 60);
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

