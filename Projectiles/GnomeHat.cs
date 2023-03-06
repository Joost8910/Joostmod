using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GnomeHat : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gnome Warrior's Hat");
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            AIType = ProjectileID.Bullet;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = Projectile.velocity.Length();
            }
            Vector2 move = Vector2.Zero;
            float distance = 250f;
            bool target = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                Vector2 newMove = npc.Center - Projectile.Center;
                float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                move = newMove;
                distance = distanceTo;
                target = true;
            }
            else for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.active && !npc.dontTakeDamage && npc.lifeMax > 5 && npc.CanBeChasedBy(this, false) && Collision.CanHit(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, npc.position, npc.width, npc.height))
                    {
                        Vector2 newMove = npc.Center - Projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                    }
                }
            if (target)
            {
                if (move.Length() > Projectile.localAI[0] && Projectile.localAI[0] > 0)
                {
                    move *= Projectile.localAI[0] / move.Length();
                }
                float home = 40f;
                Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
            }
        }
    }
}

