using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class XParasiteYellow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("X Parasite");
			Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.timeLeft = 600;
            projectile.alpha = 5;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.localAI[1] = target.whoAmI + 1;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("InfectedYellow"), 900);
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.localAI[1] > 0)
            {
                NPC target = Main.npc[(int)projectile.localAI[1] - 1];
                target.AddBuff(mod.BuffType("InfectedYellow"), 18000);
                Main.PlaySound(SoundID.NPCDeath19, projectile.Center);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.localAI[1] > 0)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool CanHitPvp(Player target)
        {
            if (projectile.localAI[1] > 0)
            {
                return false;
            }
            return base.CanHitPvp(target);
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] == 0)
            {
                projectile.scale = 0.7f + (Main.rand.Next(9) * 0.05f);
                projectile.width = (int)(28 * projectile.scale);
                projectile.height = (int)(28 * projectile.scale);
                projectile.damage = (int)(projectile.damage * projectile.scale);
                projectile.knockBack *= projectile.scale;
            }
            projectile.ai[0]++;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 6;
            }
            if (projectile.localAI[1] > 0)
            {
                NPC target = Main.npc[(int)projectile.localAI[1] - 1];
                if (target.life <= 0 || !target.active)
                {
                    projectile.localAI[1] = 0;
                }
                else
                {
                    projectile.position = target.Center - new Vector2(projectile.width / 2, 28 - projectile.scale * 14);
                    projectile.velocity = target.velocity;
                    projectile.scale -= 0.05f;
                    if (projectile.scale <= 0.1f)
                    {
                        projectile.Kill();
                    }
                }
            }
            else
            {
                Vector2 move = Vector2.Zero;
                float distance = 1200f;
                bool target = false;
                if (player.HasMinionAttackTargetNPC)
                {
                    NPC npc = Main.npc[player.MinionAttackTargetNPC];
                    Vector2 newMove = npc.Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
                else for (int k = 0; k < 200; k++)
                    {
                        NPC npc = Main.npc[k];
                        if (npc.active && !npc.dontTakeDamage && npc.lifeMax > 5 && npc.CanBeChasedBy(this, false))
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
                if (move == Vector2.Zero)
                {
                    projectile.timeLeft -= 2;
                }
                if (projectile.timeLeft < 20)
                {
                    projectile.scale -= 0.05f;
                    if (projectile.scale <= 0.1f)
                    {
                        projectile.Kill();
                    }
                }
                projectile.ai[1]++;
                if (projectile.ai[1] > 30)
                {
                    if (target)
                    {
                        projectile.localAI[0] = 10f * (1 + (1 - projectile.scale));
                        if (move.Length() > projectile.localAI[0] && projectile.localAI[0] > 0)
                        {
                            move *= projectile.localAI[0] / move.Length();
                        }
                        float home = 10f;
                        projectile.velocity = ((home - 1f) * projectile.velocity + move) / home;
                    }
                    if (projectile.velocity.Length() < projectile.localAI[0] && projectile.localAI[0] > 0)
                    {
                        projectile.velocity *= (projectile.localAI[0] / projectile.velocity.Length());
                    }
                }
            }
        }

    }
}


