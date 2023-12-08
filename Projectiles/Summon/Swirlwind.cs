using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Summon
{
    public class Swirlwind : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hurricane");
        }
        public override void SetDefaults()
        {
            Projectile.width = 360;
            Projectile.height = 360;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 16;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            int mana = 10;
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                mana = player.inventory[player.selectedItem].mana / 2;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed && player.CheckMana(mana, Projectile.ai[0] % 16 == 0);
            if (channeling)
            {
                if (Projectile.ai[0] < 1)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Vector2 vector13 = Main.MouseWorld - vector;
                        vector13.Normalize();
                        if (vector13.HasNaNs())
                        {
                            vector13 = Vector2.UnitX * player.direction;
                        }
                        if (vector13.X > 0)
                        {
                            Projectile.direction = (int)player.gravDir;
                            Projectile.netUpdate = true;
                        }
                        else
                        {
                            Projectile.direction = -(int)player.gravDir;
                            Projectile.netUpdate = true;
                        }
                    }
                }
                Projectile.ai[0]++;
            }
            else
            {
                Projectile.Kill();
            }
            if (Projectile.ai[0] % (int)(20 - Projectile.ai[1]) <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item1.WithVolumeScale(0.9f).WithPitchOffset(-0.6f), Projectile.Center);
            }
            if (Projectile.ai[1] < 12)
            {
                Projectile.ai[1] += 0.25f;
            }
            Projectile.alpha = 255 - (int)(Projectile.ai[1] * 12);
            Projectile.localNPCHitCooldown = 28 - (int)Projectile.ai[1];
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.velocity.X = Projectile.direction * 8;
            Projectile.velocity.Y = 0;
            Projectile.rotation += Projectile.ai[1] * Projectile.direction * 0.666f * 0.0174f;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            if (Projectile.ai[1] >= 12)
            {
                for (int i = 0; i < Main.item.Length; i++)
                {
                    if (Main.item[i].active)
                    {
                        Item I = Main.item[i];
                        if (Projectile.Hitbox.Intersects(I.Hitbox))
                        {
                            Vector2 vel = I.DirectionTo(Projectile.Center) * Projectile.ai[1] * 0.3125f;
                            vel = vel.RotatedBy(90f * -Projectile.direction);
                            vel += I.DirectionTo(Projectile.Center) * 2f;
                            I.velocity = vel;
                            I.position += I.velocity + player.velocity;
                        }
                    }
                }
                for (int n = 0; n < 200; n++)
                {
                    NPC target = Main.npc[n];
                    if (target.Distance(Projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                    {
                        bool tooClose = player.Distance(target.Center) <= (target.width > target.height ? target.width : target.height) + 40;
                        if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
                        {
                            Vector2 vel = target.DirectionTo(Projectile.Center) * Projectile.ai[1] * 0.625f;
                            vel = vel.RotatedBy(90f * -Projectile.direction);
                            if (tooClose)
                            {
                                vel -= target.DirectionTo(Projectile.Center) * 4f;
                            }
                            else
                            {
                                vel += target.DirectionTo(Projectile.Center) * 4f;
                            }
                            if (!target.noGravity)
                                vel.Y -= 0.3f;
                            target.velocity = vel + player.velocity * (target.knockBackResist < 0.5f ? target.knockBackResist * 2 : 1f);
                        }
                    }
                }
            }
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Projectiles/WindWheel");
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height / Main.projFrames[Projectile.type] * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Main.EntitySpriteDraw(tex, drawPosition, rect, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.Distance(Projectile.Center) < 30)
                crit = true;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (target.Distance(Projectile.Center) < 30)
                crit = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.Distance(Projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                return base.CanHitNPC(target);
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (target.Distance(Projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                return base.CanHitPvp(target);
            return false;
        }

    }
}