using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class XParasiteRed : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("X Parasite");
			Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 5;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.localAI[1] = target.whoAmI + 1;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("InfectedRed").Type, 900);
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            if (Projectile.localAI[1] > 0)
            {
                NPC target = Main.npc[(int)Projectile.localAI[1] - 1];
                target.AddBuff(Mod.Find<ModBuff>("InfectedRed").Type, 18000);
                SoundEngine.PlaySound(SoundID.NPCDeath19, Projectile.Center);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.localAI[1] > 0)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.localAI[1] > 0)
            {
                return false;
            }
            return base.CanHitPvp(target);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
                Projectile.scale = 0.7f + (Main.rand.Next(9) * 0.05f);
                Projectile.width = (int)(28 * Projectile.scale);
                Projectile.height = (int)(28 * Projectile.scale);
                Projectile.damage = (int)(Projectile.damage * Projectile.scale);
                Projectile.knockBack *= Projectile.scale;
            }
            Projectile.ai[0]++;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 6;
            }
            if (Projectile.localAI[1] > 0)
            {
                NPC target = Main.npc[(int)Projectile.localAI[1] - 1];
                if (target.life <= 0 || !target.active)
                {
                    Projectile.localAI[1] = 0;
                }
                else
                {
                    Projectile.position = target.Center - new Vector2(Projectile.width / 2, 28 - Projectile.scale * 14);
                    Projectile.velocity = target.velocity;
                    Projectile.scale -= 0.05f;
                    if (Projectile.scale <= 0.1f)
                    {
                        Projectile.Kill();
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
                    Vector2 newMove = npc.Center - Projectile.Center;
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
                if (move == Vector2.Zero)
                {
                    Projectile.timeLeft -= 2;
                }
                if (Projectile.timeLeft < 20)
                {
                    Projectile.scale -= 0.05f;
                    if (Projectile.scale <= 0.1f)
                    {
                        Projectile.Kill();
                    }
                }
                Projectile.ai[1]++;
                if (Projectile.ai[1] > 30)
                {
                    if (target)
                    {
                        Projectile.localAI[0] = 10f * (1 + (1 - Projectile.scale));
                        if (move.Length() > Projectile.localAI[0] && Projectile.localAI[0] > 0)
                        {
                            move *= Projectile.localAI[0] / move.Length();
                        }
                        float home = 10f;
                        Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                    }
                    if (Projectile.velocity.Length() < Projectile.localAI[0] && Projectile.localAI[0] > 0)
                    {
                        Projectile.velocity *= (Projectile.localAI[0] / Projectile.velocity.Length());
                    }
                }
            }
        }

    }
}


