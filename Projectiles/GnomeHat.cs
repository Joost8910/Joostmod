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
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 600;
            aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0] = projectile.velocity.Length();
            }
            Vector2 move = Vector2.Zero;
            float distance = 250f;
            bool target = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                Vector2 newMove = npc.Center - projectile.Center;
                float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                move = newMove;
                distance = distanceTo;
                target = true;
            }
            else for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.active && !npc.dontTakeDamage && npc.lifeMax > 5 && npc.CanBeChasedBy(this, false) && Collision.CanHit(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, npc.position, npc.width, npc.height))
                    {
                        Vector2 newMove = npc.Center - projectile.Center;
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
                if (move.Length() > projectile.localAI[0] && projectile.localAI[0] > 0)
                {
                    move *= projectile.localAI[0] / move.Length();
                }
                float home = 40f;
                projectile.velocity = ((home - 1f) * projectile.velocity + move) / home;
            }
        }
    }
}

