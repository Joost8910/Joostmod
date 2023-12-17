using System;
using System.IO;
using JoostMod.Projectiles.Grappling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class GrabGlove : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brawler's Glove");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
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
        float force = 5f;
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 origin = player.RotatedRelativePoint(player.MountedCenter, true);
            float speed = 12f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee);
            Projectile.localNPCHitCooldown = (int)(10 / speed);
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
                    Projectile.localAI[0] = 0;
                    Projectile.localAI[1] = -1;
                }
            }
            if (Projectile.ai[1] == 0) //Punch
            {
                Projectile.timeLeft = 3;
                if (Projectile.ai[0] > -15 && Projectile.ai[0] <= 0)
                {
                    if (Projectile.localAI[0] == 0)
                    {
                        int frame = Projectile.ai[0] < -10 ? 10 : 11;
                        player.bodyFrame.Y = frame * player.bodyFrame.Height;
                    }
                    else
                    {
                        player.bodyFrame.Y = 17 * player.bodyFrame.Height;
                    }
                    Projectile.ai[0] -= 3.75f * speed;
                }
                if (Projectile.ai[0] <= -15)
                {
                    Projectile.ai[0] = 0.1f;
                    if (Projectile.localAI[0] == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item19, Projectile.Center);
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item18, Projectile.Center);
                    }
                    Vector2 dir = Projectile.velocity;
                    dir.Normalize();
                    if (Math.Abs(dir.X) > 0.3f && (player.controlRight && player.direction > 0 || player.controlLeft && player.direction < 0) && player.velocity.X * player.direction < force)
                    {
                        player.velocity.X = player.direction * force * speed;
                    }
                    if (player.controlDown && -Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir < force)
                    {
                        player.velocity.Y = player.gravDir * force * speed;
                    }
                }
                if (Projectile.ai[0] > 0)
                {
                    Projectile.ai[0] += 3.75f * speed;
                    if (Projectile.localAI[0] == 0)
                    {
                        if (Projectile.ai[0] < 15f)
                        {
                            int frame = Projectile.ai[0] > 7.5f ? 17 : 11;
                            player.bodyFrame.Y = frame * player.bodyFrame.Height;
                        }
                    }
                    else
                    {
                        int frame = Projectile.ai[0] > 15f ? 11 : 17;
                        player.bodyFrame.Y = frame * player.bodyFrame.Height;
                    }
                }
                if (Projectile.ai[0] >= 30)
                {
                    if (player.channel)
                    {
                        Projectile.ai[0] = 0;
                        if (Projectile.localAI[0] == 0)
                        {
                            Projectile.localAI[0] = 1;
                        }
                        else
                        {
                            Projectile.localAI[0] = 0;
                        }
                    }
                    else
                    {
                        Projectile.Kill();
                    }
                }
            }
            if (player.active && !player.dead && !player.noItems && !player.CCed && player.inventory[player.selectedItem].type == Mod.Find<ModItem>("GrabGlove").Type)
            {
                if (Main.myPlayer == Projectile.owner && Projectile.ai[1] >= 0)
                {
                    float scaleFactor6 = Projectile.ai[0] * 0.5f;
                    Vector2 dir = Main.MouseWorld - origin;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * player.direction;
                    }
                    if (dir.X > 0)
                    {
                        player.ChangeDir(1);
                    }
                    if (dir.X < 0)
                    {
                        player.ChangeDir(-1);
                    }
                    Projectile.direction = player.direction;
                    if (dir.X * scaleFactor6 != Projectile.velocity.X || dir.Y * scaleFactor6 != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = dir * scaleFactor6;
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MobHook>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedMobHook>()] <= 0)
                {
                    Rectangle rect = new Rectangle((int)(player.position.X + player.velocity.X - 2), (int)(player.position.Y + player.velocity.Y - 2), player.width + 4, player.height + 4);
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        if (rect.Intersects(target.getRect()) && target.active && !target.friendly && Projectile.ai[1] != 1 && (Projectile.ai[1] == 3 || i != (int)Projectile.localAI[1]))
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
            Vector2 aim = Projectile.velocity;
            aim.Normalize();

            if (Projectile.localAI[0] == 0)
            {
                player.heldProj = Projectile.whoAmI;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction < 0 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
            if (Projectile.ai[1] == 1) //Grab
            {
                Projectile.timeLeft = 3;
                float rot = 90;
                if (Projectile.ai[0] > -15 && Projectile.ai[0] <= 0)
                {
                    if (Projectile.localAI[0] == 0)
                    {
                        int frame = Projectile.ai[0] < -10 ? 10 : 11;
                        player.bodyFrame.Y = frame * player.bodyFrame.Height;
                    }
                    else
                    {
                        player.bodyFrame.Y = 17 * player.bodyFrame.Height;
                    }
                    Projectile.ai[0] -= 1.5f * speed;
                }
                if (Projectile.ai[0] <= -15)
                {
                    Projectile.ai[0] = 0.1f;
                    SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_swing_3").WithPitchOffset(-0.2f), Projectile.Center); // 216
                }
                if (Projectile.ai[0] > 0)
                {
                    Projectile.ai[0] += 9f * speed;
                    player.velocity.X = player.direction * force * speed;
                    rot = (90 - Projectile.ai[0]) * Projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                }
                if (Projectile.ai[0] > 135)
                {
                    Projectile.ai[1] = -1;
                    Projectile.ai[0] = -1;
                    Projectile.timeLeft = 15;
                }
                Projectile.velocity = rot.ToRotationVector2() * 10;
                player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
                Projectile.rotation = Projectile.velocity.ToRotation() - 1.57f;
            }
            if (Projectile.ai[1] == 2) //NPC
            {
                Projectile.localAI[0] = 1;
                NPC target = Main.npc[(int)Projectile.localAI[1]];
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MobHook>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedMobHook>()] > 0)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == Projectile.owner && (p.type == ModContent.ProjectileType<MobHook>() || p.type == ModContent.ProjectileType<EnchantedMobHook>()))
                        {
                            p.Kill();
                            break;
                        }
                    }
                }
                if (target.active && target.life > 0)
                {
                    if (player.controlUseTile || Projectile.localAI[0] == 2)
                    {
                        if (player.controlUseItem)
                        {
                            Projectile.localAI[0] = 2;
                        }
                        else
                        {
                            Projectile.ai[0] = -1;
                        }
                        player.itemRotation = (float)Math.Atan2(aim.Y * Projectile.direction, aim.X * Projectile.direction);
                        if (target.knockBackResist > 0 && !target.boss)
                        {
                            target.position = origin + aim * 8 + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                            target.GetGlobalNPC<NPCs.JoostGlobalNPC>().immunePlayer = player.whoAmI;
                            target.velocity = player.velocity;
                            target.netUpdate = true;
                            if (Projectile.timeLeft < 2)
                            {
                                target.velocity.X = player.direction * 4;
                                target.velocity.Y = player.gravDir * -2;
                                if (player.immuneTime < 10)
                                {
                                    player.immune = true;
                                    player.immuneNoBlink = false;
                                    player.immuneTime = 10;
                                }
                                Projectile.ai[1] = -1;
                                Projectile.ai[0] = -1;
                                Projectile.timeLeft = 15;
                            }
                        }
                        else
                        {
                            Vector2 offset = new Vector2(target.width / 2 + 4, -player.height / 2);
                            if (player.direction > 0)
                            {
                                offset.X = -(player.width + 4 + target.width / 2);
                            }
                            if (Projectile.timeLeft <= 40 && !Collision.SolidCollision(new Vector2(player.Center.X, player.position.Y + player.gravDir * (40 - Projectile.timeLeft) * 0.5f), 1, player.height))
                            {
                                offset.Y += player.gravDir * (40 - Projectile.timeLeft) * 0.5f;
                            }
                            Vector2 pos = target.Center + offset;
                            if (pos.Y < 666 || pos.Y + player.height > (Main.maxTilesY - 10) * 16 || Collision.SolidCollision(pos, player.width, player.height / 2))
                            {
                                Projectile.timeLeft = 1;
                            }
                            if (Projectile.timeLeft < 2)
                            {
                                player.velocity.X = player.direction * -force * 2;
                                player.velocity.Y = player.gravDir * -3;
                                if (player.immuneTime < 10)
                                {
                                    player.immune = true;
                                    player.immuneNoBlink = false;
                                    player.immuneTime = 10;
                                }
                                Projectile.ai[1] = -1;
                                Projectile.ai[0] = -1;
                                Projectile.timeLeft = 15;
                            }
                            else
                            {
                                player.position = pos;
                                player.velocity = target.velocity;
                            }
                        }
                    }
                    else
                    {
                        if (Projectile.timeLeft < 3)
                        {
                            Projectile.timeLeft = 3;
                        }
                        Projectile.localAI[0] = 3;
                        if (target.knockBackResist > 0 && !target.boss)
                        {
                            target.GetGlobalNPC<NPCs.JoostGlobalNPC>().immunePlayer = player.whoAmI;
                            float rot = -135 + (player.direction < 0 ? 90 : 0);
                            if (Projectile.ai[0] > -15 && Projectile.ai[0] <= 0)
                            {
                                player.bodyFrame.Y = player.bodyFrame.Height;
                                Projectile.ai[0] -= 1.5f * speed;
                            }
                            if (Projectile.ai[0] <= -15)
                            {
                                Projectile.ai[0] = 0.1f;
                                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_1"), Projectile.Center); // 214
                            }
                            if (Projectile.ai[0] > 0)
                            {
                                Projectile.ai[0] += 20f * speed;
                                rot = (-135 + Projectile.ai[0]) * Projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                            }
                            Projectile.velocity = rot.ToRotationVector2() * 8;
                            target.position = origin + Projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57f;
                            if (Projectile.ai[0] > 90)
                            {
                                target.velocity = player.velocity + aim * Projectile.knockBack * 3;
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)JoostModMessageType.NPCpos);
                                    packet.Write(target.whoAmI);
                                    packet.WriteVector2(target.position);
                                    packet.WriteVector2(target.velocity);
                                    ModPacket netMessage = packet;
                                    netMessage.Send(-1, player.whoAmI);
                                }
                                if (Main.myPlayer == Projectile.owner)
                                    Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, target.velocity, ModContent.ProjectileType<GrabThrow>(), (int)(Projectile.damage * 4f), Projectile.knockBack, Projectile.owner, target.whoAmI);
                                if (player.immuneTime < 10)
                                {
                                    player.immune = true;
                                    player.immuneNoBlink = false;
                                    player.immuneTime = 10;
                                }
                                Projectile.ai[1] = -1;
                                Projectile.ai[0] = -1;
                                Projectile.timeLeft = 15;
                            }
                        }
                        else
                        {
                            player.velocity.X = player.direction * -force * 2;
                            player.velocity.Y = player.gravDir * -3;
                            if (player.immuneTime < 10)
                            {
                                player.immune = true;
                                player.immuneNoBlink = false;
                                player.immuneTime = 10;
                            }
                            Projectile.ai[1] = -1;
                            Projectile.ai[0] = -1;
                            Projectile.timeLeft = 15;
                        }
                    }
                }
                else
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[1] == 3) //Pvp
            {
                Projectile.localAI[0] = 3;
                Player target = Main.player[(int)Projectile.localAI[1]];
                if (target.active && target.statLife > 0)
                {
                    target.noKnockback = true;
                    if (player.controlUseTile || Projectile.localAI[0] == 2)
                    {
                        target.position = origin + aim * 8 + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        if (player.controlUseItem)
                        {
                            Projectile.localAI[0] = 2;
                        }
                        else
                        {
                            Projectile.ai[0] = -1;
                        }
                        player.itemRotation = (float)Math.Atan2(aim.Y * Projectile.direction, aim.X * Projectile.direction);
                    }
                    else
                    {
                        if (Projectile.timeLeft < 3)
                        {
                            Projectile.timeLeft = 3;
                        }
                        Projectile.localAI[0] = 1;
                        float rot = -135 + (player.direction < 0 ? 90 : 0);
                        if (Projectile.ai[0] > -15 && Projectile.ai[0] <= 0)
                        {
                            player.bodyFrame.Y = player.bodyFrame.Height;
                            Projectile.ai[0] -= 1.5f * speed;
                        }
                        if (Projectile.ai[0] <= -15)
                        {
                            Projectile.ai[0] = 0.1f;
                            SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_1"), Projectile.Center); // 214
                        }
                        if (Projectile.ai[0] > 0)
                        {
                            Projectile.ai[0] += 10f * speed;
                            rot = (-135 + Projectile.ai[0]) * Projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                        }
                        Projectile.velocity = rot.ToRotationVector2() * 8;
                        target.position = origin + Projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        Projectile.rotation = Projectile.velocity.ToRotation() - 1.57f;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            aim = player.DirectionTo(Main.MouseWorld);
                            Projectile.netUpdate = true;
                        }
                        if (Projectile.ai[0] > 90)
                        {
                            target.velocity = player.velocity + aim * Projectile.knockBack * 3;
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                ModPacket packet = Mod.GetPacket();
                                packet.Write((byte)JoostModMessageType.Playerpos);
                                packet.Write((byte)target.whoAmI);
                                packet.WriteVector2(target.position);
                                packet.WriteVector2(player.velocity + aim * Projectile.knockBack * 3);
                                ModPacket netMessage = packet;
                                netMessage.Send(-1, -1);

                                ModPacket packet2 = Mod.GetPacket();
                                packet2.Write((byte)JoostModMessageType.Playerpos);
                                packet2.Write((byte)Projectile.owner);
                                packet2.WriteVector2(player.position);
                                packet2.WriteVector2(player.velocity);
                                ModPacket netMessage2 = packet2;
                                netMessage2.Send(-1, Projectile.owner);
                            }
                            if (Main.myPlayer == Projectile.owner)
                                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, player.velocity + aim * Projectile.knockBack * 3, ModContent.ProjectileType<GrabThrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner, -1, target.whoAmI);
                            if (player.immuneTime < 10)
                            {
                                player.immune = true;
                                player.immuneNoBlink = false;
                                player.immuneTime = 10;
                            }
                            Projectile.ai[1] = -1;
                            Projectile.ai[0] = -1;
                            Projectile.timeLeft = 15;
                        }
                    }
                }
                else
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.localAI[0] == 2 && (Projectile.ai[1] == 2 || Projectile.ai[1] == 3)) //Pummel
            {
                Projectile.localAI[0] = 2;
                if (Projectile.ai[0] > -15 && Projectile.ai[0] <= 0)
                {
                    Projectile.ai[0] -= 3f * speed;
                }
                if (Projectile.ai[0] <= -15)
                {
                    Projectile.ai[0] = 0.1f;
                    SoundEngine.PlaySound(SoundID.Item18, Projectile.Center);
                }
                if (Projectile.ai[0] > 0)
                {
                    Projectile.ai[0] += 3f * speed;
                }
                if (Projectile.ai[0] >= 30)
                {
                    Projectile.ai[0] = -1f;
                    Projectile.localAI[0] = 1;
                }
                Vector2 dir = Projectile.velocity;
                dir.Normalize();
                Projectile.velocity = dir * Projectile.ai[0] * 0.5f;
            }
            Projectile.position = Projectile.velocity + origin - Projectile.Size / 2f;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (Projectile.ai[0] >= 1)
            {
                Vector2 drawOrigin = new Vector2(tex.Width / 2, tex.Height / 2);
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                    Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
                    Main.EntitySpriteDraw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                }
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly && Projectile.ai[0] >= 1;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.ai[0] >= 1)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.ai[1] == 1)
            {
                crit = true;
            }
            if (target.knockBackResist > 0 && target.life < (damage - target.defense / 2) * (crit ? 2 : 1) && (Projectile.ai[1] == 1 || Projectile.localAI[0] == 3))
            {
                damage = (target.life + target.defense / 2 - 3) / (crit ? 2 : 1);
            }
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[1] == 0 && !player.controlLeft && !player.controlRight)
            {
                knockback = 0;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.ai[1] == 1)
            {
                crit = true;
            }
            if (target.statLife < (damage - target.statDefense / 2) * (crit ? 2 : 1) && (Projectile.ai[1] == 1 || Projectile.localAI[0] == 3))
            {
                damage = (target.statLife + target.statDefense / 2 - 3) / (crit ? 2 : 1);
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
            if (Projectile.ai[1] == 1 && target.life > 0)
            {
                Projectile.timeLeft = 240;
                Projectile.localAI[1] = target.whoAmI;
                Projectile.ai[0] = -1;
                Projectile.ai[1] = 2;
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_2"), Projectile.Center); // 215
                if (player.immuneTime < 20)
                {
                    player.immune = true;
                    player.immuneNoBlink = false;
                    player.immuneTime = 20;
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
                        player.velocity = -dir * force;
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
                    player.velocity = -dir * (Projectile.knockBack + (incoming ? target.velocity.Length() : 0));
                    if (player.immuneTime < 2)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 2;
                    }
                }
            }
            if (Projectile.ai[1] == 1 && target.statLife > 0 && target.ownedProjectileCounts[Projectile.type] + target.ownedProjectileCounts[ModContent.ProjectileType<Stonefist2>()] <= 0)
            {
                Projectile.timeLeft = target.noKnockback ? 50 : 100;
                Projectile.localAI[1] = target.whoAmI;
                Projectile.ai[0] = -1;
                Projectile.ai[1] = 3;
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_2"), Projectile.Center); // 215
                if (player.immuneTime < 20)
                {
                    player.immune = true;
                    player.immuneNoBlink = false;
                    player.immuneTime = 20;
                }
            }
        }
    }
}