using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Claw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twin Claws");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 28;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 7;
            Projectile.timeLeft = 7;
            Projectile.scale = 0.9f;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)Projectile.localAI[0]);
            writer.Write((short)Projectile.localAI[1]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadInt16();
            Projectile.localAI[1] = reader.ReadInt16();
        }
        float force = 3.5f;
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 origin = player.RotatedRelativePoint(player.MountedCenter, true);

            bool channeling = (Projectile.ai[1] == 1 || player.channel) && !player.noItems && !player.CCed && !player.dead && player.HeldItem.shoot == Projectile.type;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - origin;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                }
                Projectile.timeLeft = 2;

                if (player.ownedProjectileCounts[ModContent.ProjectileType<MobHook>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedMobHook>()] <= 0 && (Projectile.ai[1] != 1 || Projectile.localAI[1] < 0))
                {
                    Rectangle rect = new Rectangle((int)(player.position.X + player.velocity.X - 2), (int)(player.position.Y + player.velocity.Y - 2), player.width + 4, player.height + 4);
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        if (rect.Intersects(target.getRect()) && target.active && !target.friendly)
                        {
                            if (player.velocity.Y < 0 && Projectile.velocity.Y < 0 && player.Center.Y >= target.Center.Y ||
                                player.velocity.Y > 0 && Projectile.velocity.Y > 0 && player.Center.Y <= target.Center.Y ||
                                player.velocity.X >= 0 && player.direction > 0 && player.Center.X <= target.Center.X ||
                                player.velocity.X <= 0 && player.direction < 0 && player.Center.X >= target.Center.X)
                            {
                                bool incoming = false;
                                if (target.velocity.Y >= 0 && player.Center.Y >= target.Center.Y ||
                                    target.velocity.Y <= 0 && player.Center.Y <= target.Center.Y ||
                                    target.velocity.X >= 0 && player.Center.X >= target.Center.X ||
                                    target.velocity.X <= 0 && player.Center.X <= target.Center.X)
                                {
                                    incoming = true;
                                }
                                Vector2 dir = target.Center - player.Center;
                                dir.Normalize();
                                if (target.knockBackResist > 0)
                                {
                                    target.velocity = dir + player.velocity;
                                    if (!target.noTileCollide)
                                    {
                                        Vector2 push = new Vector2(player.Center.X + rect.Width / 2, target.position.Y);
                                        if (Projectile.direction < 0)
                                        {
                                            push.X = player.Center.X - rect.Width / 2 - target.width;
                                        }
                                        if (dir.Y > 0.7f)
                                        {
                                            push.Y = target.position.Y + 16;
                                        }
                                        if (dir.Y < -0.7f)
                                        {
                                            push.Y = target.position.Y - 16;
                                        }
                                        Vector2 pos = push + player.velocity;
                                        if (Collision.SolidCollision(pos, target.width, target.height))
                                        {
                                            player.velocity = -dir;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    player.velocity = -dir + (incoming ? target.velocity : Vector2.Zero);
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                Projectile.Kill();
            }
            float speed = 7f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee);
            if (Projectile.velocity.Y * player.gravDir >= 0)
            {
                origin.Y += 4 * player.gravDir;
            }
            if (Projectile.ai[0] == 0)
            {
                if (player.controlUseItem)
                {
                    Projectile.ai[1] = 0;
                }
                if (player.controlUseTile)
                {
                    Projectile.ai[1] = 1;
                    Projectile.localAI[1] = -4;
                }
                Projectile.ai[0] = 1;
            }
            if (Projectile.ai[1] == 0) //Rapid Slash
            {
                Projectile.localAI[0] += speed;
                Projectile.localAI[1] += speed;
                if (Projectile.localAI[0] >= 1)
                {
                    Projectile.frame = (Projectile.frame + 1) % 28;
                    Projectile.localAI[0] -= 1f;
                }
                Projectile.localNPCHitCooldown = (int)(7 / speed);
                Projectile.soundDelay--;
                if (Projectile.soundDelay <= 0)
                {
                    if (Projectile.ai[0] == 2) //Offhand
                    {
                        SoundEngine.PlaySound(SoundID.Item1.WithVolumeScale(0.7f).WithPitchOffset(-0.2f), origin);
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item1.WithVolumeScale(0.85f), origin);
                    }
                    Projectile.soundDelay = (int)(7 / speed);
                }
                if (Projectile.frame % 7 == 2)
                {
                    Vector2 dir = Projectile.velocity;
                    dir.Normalize();
                    if (Math.Abs(dir.X) > 0.3f && (player.controlRight && player.direction > 0 || player.controlLeft && player.direction < 0) && player.velocity.X * player.direction < force)
                    {
                        player.velocity.X = player.direction * force;
                    }
                    if (player.controlDown && Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir < force)
                    {
                        player.velocity.Y = player.gravDir * force;
                    }
                }
                if (Projectile.ai[0] != 2)
                {
                    if (player.ownedProjectileCounts[Projectile.type] < 2 && Projectile.localAI[1] >= 3)
                    {
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), origin, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 2, 0);
                    }
                }
            }
            if (Projectile.ai[1] == 1) //Leaping Slash
            {
                Projectile.localNPCHitCooldown = -1;
                Projectile.localAI[1] += speed * 0.5f;

                if (Projectile.localAI[1] < 0)
                {
                    player.velocity.X *= 0.9f;
                }
                else
                {
                    Projectile.direction = Math.Sign(Projectile.velocity.X);
                    Projectile.velocity.X = 4 * Projectile.direction;
                    Projectile.velocity.Y = Projectile.localAI[1] * 0.5f * player.gravDir;
                    if (Projectile.localAI[0] == 0)
                    {
                        if (Projectile.ai[0] != 2 && player.ownedProjectileCounts[Projectile.type] < 2)
                        {
                            Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), origin, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 2, 1);
                            if (player.velocity.Y == 0)
                            {
                                player.velocity.Y = -player.gravDir * 6;
                            }
                            if (player.velocity.X * Projectile.direction < 10)
                            {
                                player.velocity.X = Projectile.direction * 10;
                            }
                            SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_1"), Projectile.Center); // 214
                            SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_2"), Projectile.Center); // 215
                        }
                        Projectile.localAI[0] = 1;
                    }

                    Projectile.frame = (int)(Projectile.localAI[1] + 14);
                    if (Projectile.localAI[1] >= 7)
                    {
                        Projectile.Kill();
                    }
                }
            }

            Projectile.position = Projectile.velocity + origin - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;

            if (Projectile.ai[0] != 2)
            {
                player.ChangeDir(Projectile.direction);
                //player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
                player.heldProj = Projectile.whoAmI;
                player.itemTime = (int)((7f - Projectile.localAI[1] % 7) / speed);
                player.itemAnimation = (int)((7f - Projectile.localAI[1] % 7) / speed);
            }
            return false;
        }
        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return Projectile.frame % 7 > 1 && Projectile.frame % 7 < 5 || Projectile.ai[1] == 1 && Projectile.localAI[1] >= 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            if (Projectile.ai[0] == 2)
            {
                tex = Mod.Assets.Request<Texture2D>("Projectiles/Melee/Claw2").Value;
            }
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (Projectile.localAI[1] >= 0)
            {
                Vector2 drawOrigin = new Vector2(tex.Width / 2, tex.Height / Main.projFrames[Projectile.type] / 2);
                Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
                if (Projectile.ai[1] >= 1)
                {
                    for (int k = 0; k < Projectile.oldPos.Length; k++)
                    {
                        Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                        Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                        Main.EntitySpriteDraw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                    }
                }
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, rect, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.ai[1] == 1)
            {
                damage = (int)(damage * 1.25f);
                crit = true;
            }
            Player player = Main.player[Projectile.owner];
            if (!player.controlLeft && !player.controlRight)
            {
                knockback = 0;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.ai[1] == 1)
            {
                damage = (int)(damage * 1.25f);
                crit = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 dir = Projectile.velocity;
            dir.Normalize();
            if (Projectile.ai[1] == 0)
            {
                if (target.knockBackResist > 0)
                {
                    target.velocity = dir * knockback * target.knockBackResist;
                    if (Vector2.Distance(target.position, player.position + player.velocity) < Vector2.Distance(target.position, player.position))
                    {
                        target.velocity += player.velocity;
                    }
                    if (!target.noTileCollide)
                    {
                        Vector2 push = new Vector2(Projectile.Center.X + 16, target.position.Y);
                        if (Projectile.direction < 0)
                        {
                            push.X = Projectile.Center.X - 16 - target.width;
                        }
                        if (dir.Y > 0.3f)
                        {
                            push.Y = target.position.Y + 16;
                        }
                        if (dir.Y < -0.3f)
                        {
                            push.Y = target.position.Y - 16;
                        }
                        Vector2 pos = push + player.velocity;
                        if (Collision.SolidCollision(pos, target.width, target.height))
                        {
                            player.velocity = -dir * force;
                            if (player.immuneTime < 2)
                            {
                                player.immune = true;
                                player.immuneNoBlink = true;
                                player.immuneTime = 2;
                            }
                        }
                    }
                }
                else
                {
                    bool incoming = false;
                    if (target.velocity.Y > 0 && player.Center.Y > target.Center.Y ||
                        target.velocity.Y < 0 && player.Center.Y < target.Center.Y ||
                        target.velocity.X > 0 && player.Center.X > target.Center.X ||
                        target.velocity.X < 0 && player.Center.X < target.Center.X)
                    {
                        incoming = true;
                    }
                    player.velocity = -dir * (force + (incoming ? target.velocity.Length() : 0));
                    if (player.immuneTime < 2)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 2;
                    }
                }
            }
            else
            {
                if (player.immuneTime < 10)
                {
                    player.immune = true;
                    player.immuneTime = 10;
                }
                if (target.knockBackResist == 0)
                {
                    bool incoming = false;
                    if (target.velocity.Y > 0 && player.Center.Y > target.Center.Y ||
                        target.velocity.Y < 0 && player.Center.Y < target.Center.Y ||
                        target.velocity.X > 0 && player.Center.X > target.Center.X ||
                        target.velocity.X < 0 && player.Center.X < target.Center.X)
                    {
                        incoming = true;
                    }
                    player.velocity = -dir * (force * 2 + (incoming ? target.velocity.Length() : 0));
                }
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 dir = Projectile.velocity;
            dir.Normalize();
            if (Projectile.ai[1] == 0)
            {
                if (!target.noKnockback)
                {
                    target.velocity = dir * (Projectile.knockBack + player.velocity.Length());
                    float push = Projectile.Center.X + 16;
                    if (Projectile.direction < 0)
                    {
                        push = Projectile.Center.X - 16 - target.width;
                    }
                    Vector2 pos = target.position;
                    pos.X = push + player.velocity.X;
                    if (Collision.SolidCollision(pos, target.width, target.height))
                    {
                        player.velocity = -dir * 2;
                        if (player.immuneTime < 2)
                        {
                            player.immune = true;
                            player.immuneNoBlink = true;
                            player.immuneTime = 2;
                        }
                    }
                }
                else
                {
                    bool incoming = false;
                    if (target.velocity.Y > 0 && player.Center.Y > target.Center.Y ||
                        target.velocity.Y < 0 && player.Center.Y < target.Center.Y ||
                        target.velocity.X > 0 && player.Center.X > target.Center.X ||
                        target.velocity.X < 0 && player.Center.X < target.Center.X)
                    {
                        incoming = true;
                    }
                    player.velocity = -dir * (force + (incoming ? target.velocity.Length() : 0));
                    if (player.immuneTime < 2)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 2;
                    }
                }
            }
            else
            {
                if (player.immuneTime < 10)
                {
                    player.immune = true;
                    player.immuneTime = 10;
                }
            }
        }
    }
}