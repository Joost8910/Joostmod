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
    public class Stonefist2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Hand");
            Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 82;
            Projectile.height = 82;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 27;
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
        private void JumpOff(Player player)
        {
            player.velocity.X = player.direction * -6;
            player.velocity.Y = player.gravDir * -10;
            player.wingTime = player.wingTimeMax;
            if (player.hasJumpOption_Cloud)
            {
                player.canJumpAgain_Cloud = true;
            }
            if (player.hasJumpOption_Sandstorm)
            {
                player.canJumpAgain_Sandstorm = true;
            }
            if (player.hasJumpOption_Blizzard)
            {
                player.canJumpAgain_Blizzard = true;
            }
            if (player.hasJumpOption_Fart)
            {
                player.canJumpAgain_Fart = true;
            }
            if (player.hasJumpOption_Sail)
            {
                player.canJumpAgain_Sail = true;
            }
            if (player.hasJumpOption_Unicorn)
            {
                player.canJumpAgain_Unicorn = true;
            }
            if (player.immuneTime < 10)
            {
                player.immune = true;
                player.immuneNoBlink = false;
                player.immuneTime = 10;
            }
            Projectile.ai[1] = 0;
            Projectile.ai[0] = -1;
            Projectile.timeLeft = 15;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 origin = player.RotatedRelativePoint(player.MountedCenter, true);
            float speed = 55f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee);
            Projectile.localNPCHitCooldown = (int)(27 / speed);
            if (player.active && !player.dead && !player.noItems && !player.CCed)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == ModContent.ProjectileType<Stonefist>())
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale * (Projectile.ai[0] * 0.5f + 1.75f);
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
            }
            else
            {
                Projectile.Kill();
            }
            Vector2 aim = Projectile.velocity;
            aim.Normalize();

            if (Projectile.soundDelay <= 0 && Projectile.soundDelay > -10)
            {
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_swing_3").WithPitchOffset(-0.3f), Projectile.Center); // 216
                Projectile.soundDelay = -10;
            }


            if (Projectile.ai[1] == 1) //Grab
            {
                Projectile.timeLeft = 3;
                Projectile.ai[0] += 0.2f * speed;
                Vector2 dir = Projectile.velocity;
                dir.Normalize();
                dir = dir * 10f * (Projectile.ai[1] + 0.75f) * speed;
                if (Projectile.ai[0] > 2)
                {
                    Projectile.ai[1] = -1;
                    Projectile.ai[0] = -1;
                    Projectile.timeLeft = 15;
                }
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
                    if (player.controlUseItem || Projectile.localAI[0] == 2)
                    {
                        if (player.controlUseTile)
                        {
                            Projectile.localAI[0] = 2;
                        }
                        else
                        {
                            Projectile.ai[0] = 0;
                        }
                        player.itemRotation = (float)Math.Atan2(aim.Y * Projectile.direction, aim.X * Projectile.direction);
                        if (target.knockBackResist > 0)
                        {
                            target.position = Projectile.Center + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
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
                            JumpOff(player);
                        }
                    }
                    else
                    {
                        if (Projectile.timeLeft < 3)
                        {
                            Projectile.timeLeft = 3;
                        }
                        Projectile.localAI[0] = 3;
                        if (target.knockBackResist > 0)
                        {
                            target.GetGlobalNPC<NPCs.JoostGlobalNPC>().immunePlayer = player.whoAmI;
                            float rot = -135 + (player.direction < 0 ? 90 : 0);
                            if (Projectile.ai[0] > -1 && Projectile.ai[0] <= 0)
                            {
                                player.bodyFrame.Y = player.bodyFrame.Height;
                                Projectile.ai[0] -= 0.1f * speed;
                            }
                            if (Projectile.ai[0] <= -1)
                            {
                                Projectile.ai[0] = 0.1f;
                                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_1"), Projectile.Center); // 214
                            }
                            if (Projectile.ai[0] > 0)
                            {
                                Projectile.ai[0] += 0.4f * speed;
                                rot = (-135 + Projectile.ai[0] * 45) * Projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                            }
                            Projectile.velocity = rot.ToRotationVector2() * 8;
                            target.position = Projectile.Center + Projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);

                            if (Projectile.ai[0] > 2)
                            {
                                target.velocity = player.velocity + aim * Projectile.knockBack;
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
                                Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, target.velocity, ModContent.ProjectileType<GrabThrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner, target.whoAmI);
                                if (player.immuneTime < 10)
                                {
                                    player.immune = true;
                                    player.immuneNoBlink = false;
                                    player.immuneTime = 10;
                                }
                                Projectile.ai[1] = 0;
                                Projectile.ai[0] = -1;
                                Projectile.timeLeft = 15;
                                Projectile.velocity = new Vector2(player.direction, 0);
                                Projectile.Center = origin;
                            }
                        }
                        else
                        {
                            JumpOff(player);
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
                    if (player.controlUseItem || Projectile.localAI[0] == 2)
                    {
                        target.position = Projectile.Center + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        if (player.controlUseTile)
                        {
                            Projectile.localAI[0] = 2;
                        }
                        else
                        {
                            Projectile.ai[0] = 0;
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
                        if (Projectile.ai[0] > -1 && Projectile.ai[0] <= 0)
                        {
                            player.bodyFrame.Y = player.bodyFrame.Height;
                            Projectile.ai[0] -= 0.1f * speed;
                        }
                        if (Projectile.ai[0] <= -1)
                        {
                            Projectile.ai[0] = 0.1f;
                            SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_1"), Projectile.Center); // 214
                        }
                        if (Projectile.ai[0] > 0)
                        {
                            Projectile.ai[0] += 0.4f * speed;
                            rot = (-135 + Projectile.ai[0] * 45) * Projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                        }
                        Projectile.velocity = rot.ToRotationVector2() * 8;
                        target.position = Projectile.Center + Projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        if (Main.myPlayer == Projectile.owner)
                        {
                            aim = player.DirectionTo(Main.MouseWorld);
                            Projectile.netUpdate = true;
                        }
                        if (Projectile.ai[0] > 2)
                        {
                            target.velocity = player.velocity + aim * Projectile.knockBack;
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                ModPacket packet = Mod.GetPacket();
                                packet.Write((byte)JoostModMessageType.Playerpos);
                                packet.Write((byte)target.whoAmI);
                                packet.WriteVector2(target.position);
                                packet.WriteVector2(player.velocity + aim * Projectile.knockBack);
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
                            Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, player.velocity + aim * Projectile.knockBack, ModContent.ProjectileType<GrabThrow>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner, -1, target.whoAmI);
                            if (player.immuneTime < 10)
                            {
                                player.immune = true;
                                player.immuneNoBlink = false;
                                player.immuneTime = 10;
                            }
                            Projectile.ai[1] = 0;
                            Projectile.ai[0] = -1;
                            Projectile.timeLeft = 15;
                            Projectile.velocity = new Vector2(player.direction, 0);
                            Projectile.Center = origin;
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
                if (Projectile.ai[0] > -1 && Projectile.ai[0] <= 0)
                {
                    Projectile.ai[0] -= 0.05f * speed;
                }
                if (Projectile.ai[0] <= -1)
                {
                    Projectile.ai[0] = 0.05f;
                    SoundEngine.PlaySound(SoundID.Item18, Projectile.Center);
                }
                if (Projectile.ai[0] > 0)
                {
                    Projectile.ai[0] += 0.1f * speed;
                }
                if (Projectile.ai[0] > 1)
                {
                    Projectile.ai[0] = 0;
                    Projectile.localAI[0] = 1;
                }
            }
            if (Projectile.ai[1] == 1 || Projectile.ai[1] == 0)
            {
                Projectile.frame = 0;
            }
            else
            {
                Projectile.frame = 1;
            }

            Projectile.position = Projectile.velocity + origin - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f + Projectile.direction * 0.785f;
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.localAI[0] != 3)
            {
                player.ChangeDir(Projectile.direction);
            }
            //player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
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
            Vector2 drawOrigin = new Vector2(tex.Width / 2, tex.Height / Main.projFrames[Projectile.type] / 2);
            if (Projectile.ai[1] >= 1)
            {
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                    Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
                    Main.EntitySpriteDraw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                }
            }
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type])), lightColor, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);

            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly && Projectile.ai[0] > 0;
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
            if (target.knockBackResist > 0 && target.life < (damage - target.defense / 2) * (crit ? 2 : 1) && (Projectile.ai[1] == 1 || Projectile.localAI[0] == 3))
            {
                damage = (target.life + target.defense / 2 - 3) / (crit ? 2 : 1);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (target.statLife < (damage - target.statDefense / 2) * (crit ? 2 : 1) && (Projectile.ai[1] == 1 || Projectile.localAI[0] == 3))
            {
                damage = (target.statLife + target.statDefense / 2 - 3) / (crit ? 2 : 1);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[1] == 1 && target.life > 0 && target.knockBackResist > 0)
            {
                Projectile.timeLeft = 120;
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
            if (Projectile.ai[1] == 1 && target.statLife > 0 && target.ownedProjectileCounts[Projectile.type] + target.ownedProjectileCounts[ModContent.ProjectileType<GrabGlove>()] <= 0)
            {
                Projectile.localAI[1] = target.whoAmI;
                Projectile.ai[0] = -1;
                Projectile.ai[1] = 3;
                Projectile.timeLeft = target.noKnockback ? 60 : 120;
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