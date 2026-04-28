using JoostMod.Buffs;
using JoostMod.NPCs.Bosses.SAX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SAXMissile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Missile");
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 660;
            Projectile.extraUpdates = 1;
            //AIType = ProjectileID.Bullet;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }
        public override void AI()
        {
            bool target = false;
            if (Projectile.ai[0] > 0)
            {
                Vector2 dustOffset = (Projectile.rotation - 1.57f).ToRotationVector2() * 16;
                if (Projectile.ai[0] < 90 && Projectile.ai[0] % 2 == 0)
                {
                    Dust.NewDustPerfect(Projectile.Center - dustOffset, DustID.Smoke, Vector2.Zero);

                    Dust.NewDustDirect(Projectile.Center - dustOffset, 1, 1, DustID.Flare, 0, 0, 0, default, 1f).noGravity = true;
                }
                else
                {
                    Dust.NewDustDirect(Projectile.Center - dustOffset, 1, 1, DustID.Flare, 0, 0, 0, default, 2f).noGravity = true;
                }
                if (Projectile.timeLeft < 600)
                {
                    Projectile.timeLeft = 600;
                }
                Projectile.ai[0]--;

                Vector2 move = Vector2.Zero;
                float distance = 800f;
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (Projectile.Distance(player.Center) > distance || !player.active || player.dead)
                    {
                        player = Main.player[k];
                    }
                    if (player.active && !player.dead && Collision.CanHit(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, player.position, player.width, player.height))
                    {
                        Vector2 newMove = player.Center - Projectile.Center;
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
                    float maxSpeed = 10f;
                    move.Normalize();
                    Projectile.velocity += move * 0.2f;
                    if (Projectile.velocity.Length() > maxSpeed)
                    {
                        Projectile.velocity *= maxSpeed / Projectile.velocity.Length();
                    }
                    if (Projectile.timeLeft > 600)
                    {
                        Projectile.rotation = MathHelper.Lerp(Projectile.velocity.ToRotation(), move.ToRotation(), (660 - Projectile.timeLeft) / 60f) + 1.57f;
                    }
                    else
                    {
                        Projectile.rotation = move.ToRotation() + 1.57f;
                    }
                }
            }
            else if (Projectile.velocity.Y < 10)
            {
                Projectile.velocity.Y += 0.15f;
            }
            if (!target)
            {
                Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.velocity.ToRotation() + 1.57f, 0.5f);
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire3, 600);
            Projectile.Kill();
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= JoostFunctions.GameDamageMult() * 2;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!(target.type == ModContent.NPCType<SAX>() || target.type == ModContent.NPCType<XParasite>()))
            {
                return true;
            }
            return base.CanHitNPC(target);
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<SAXExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileExplosion"), Projectile.Center);
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
}

