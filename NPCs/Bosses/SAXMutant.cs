using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Buffs;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class SAXMutant : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SA-X");
            Main.npcFrameCount[NPC.type] = 9;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 4;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused,
                    BuffID.Frostburn,
                    BuffID.Frostburn2,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    ModContent.BuffType<InfectedRed>(),
                    ModContent.BuffType<InfectedGreen>(),
                    ModContent.BuffType<InfectedBlue>(),
                    ModContent.BuffType<InfectedYellow>()
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 170;
            NPC.damage = 125;
            NPC.defense = 16;
            NPC.lifeMax = 200000;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath9;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = false;
            NPC.noGravity = true;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/VsSax");
            NPC.frameCounter = 0;
            NPC.scale = 0.625f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
        public override bool PreKill()
        {
            return false;
        }


        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)NPC.localAI[0]);
            writer.Write((int)NPC.localAI[1]);
            writer.Write((int)NPC.localAI[2]);
            writer.Write((int)NPC.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadInt32();
            NPC.localAI[1] = reader.ReadInt32();
            NPC.localAI[2] = reader.ReadInt32();
            NPC.localAI[3] = reader.ReadInt32();
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.localAI[2] > 0)
            {
                NPC.frameCounter++;
                if (NPC.localAI[2] < 6)
                {
                    NPC.frame.Y = 0;
                }
                NPC.frame.X = 540;
                if (NPC.frameCounter >= 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + 200);
                    if (NPC.frame.Y > 1600)
                    {
                        NPC.frame.Y = 1600;
                    }
                }
            }
            else if (NPC.localAI[3] >= 100)
            {
                NPC.frameCounter++;
                if (NPC.localAI[3] < 110 || NPC.localAI[3] == 230 || NPC.localAI[3] == 280)
                {
                    NPC.frame.Y = 0;
                }
                NPC.frame.X = 720;
                if (NPC.localAI[3] >= 230)
                {
                    NPC.frame.X = 900;
                }
                if (NPC.localAI[3] >= 280)
                {
                    NPC.frame.X = 1080;
                }
                if (NPC.frameCounter >= 10 || (NPC.frameCounter >= 6 && NPC.localAI[3] > 240))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + 200);
                    if (NPC.frame.Y > 1600)
                    {
                        NPC.frame.Y = 1600;
                    }
                }
            }
            else if (NPC.ai[1] > 0)
            {
                NPC.frameCounter++;
                if (NPC.ai[1] == 1)
                {
                    NPC.frame.Y = 0;
                }
                NPC.frame.X = 360;
                if (NPC.frameCounter >= 6)
                {
                    NPC.frameCounter = 0;
                    if (NPC.velocity.Y < 0)
                    {
                        NPC.frame.Y = NPC.frame.Y <= 1400 ? 1600 : 1400;
                    }
                    else
                    {
                        NPC.frame.Y = (NPC.frame.Y + 200);
                        if (NPC.frame.Y > 1000)
                        {
                            NPC.frame.Y = 0;
                        }
                        if (NPC.ai[1] > 36)
                        {
                            NPC.frame.Y = 1200;
                        }
                    }
                }
            }
            else if (NPC.ai[2] > 0)
            {
                NPC.frame.X = 180;
                NPC.frameCounter++;
                if (NPC.ai[2] < 10)
                {
                    NPC.frame.Y = 800;
                }
                else if (NPC.ai[2] < 40)
                {
                    if (NPC.frameCounter >= 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = NPC.frame.Y <= 1000 ? 1200 : 1000;
                    }
                }
                else if (NPC.ai[2] < 100)
                {
                    if (NPC.ai[2] == 100)
                    {
                        NPC.frame.Y = 1400;
                    }
                    if (NPC.frameCounter >= 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = NPC.frame.Y <= 1400 ? 1600 : 1400;
                    }
                }
                else
                {
                    NPC.frame.Y = 800;
                }
            }
            else if (NPC.ai[3] > 0)
            {
                NPC.frame.X = 180;
                if (NPC.ai[3] < 7)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.ai[3] < 14)
                {
                    NPC.frame.Y = 200;
                }
                else if (NPC.ai[3] < 21)
                {
                    NPC.frame.Y = 400;
                }
                else
                {
                    NPC.frame.Y = 600;
                }
            }
            else if (NPC.ai[0] > 0)
            {
                NPC.frame.X = 0;
                if (NPC.ai[0] < 7)
                {
                    NPC.frame.Y = 800;
                }
                else if (NPC.ai[0] < 14)
                {
                    NPC.frame.Y = 1000;
                }
                else if (NPC.ai[0] < 21)
                {
                    NPC.frame.Y = 1200;
                }
                else if (NPC.ai[0] < 28)
                {
                    NPC.frame.Y = 1400;
                }
                else
                {
                    NPC.frame.Y = 1600;
                }
            }
            else
            {
                NPC.frameCounter++;
                NPC.frame.X = 0;
                if (NPC.frameCounter >= 9)
                {
                    NPC.frameCounter = 0;
                    if (NPC.velocity.Y < 0)
                    {
                        NPC.frame.X = 360;
                        NPC.frame.Y = NPC.frame.Y <= 1400 ? 1600 : 1400;
                    }
                    else
                    {
                        NPC.frame.Y = (NPC.frame.Y + 200);
                        if (NPC.frame.Y > 600)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.localAI[0] > 60)
            {
                return base.CanHitPlayer(target, ref cooldownSlot);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (NPC.localAI[0] > 60)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode && (NPC.ai[1] >= 100 || NPC.ai[2] >= 20))
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.mount.Dismount(target);
                if (NPC.ai[1] >= 120)
                {
                    target.velocity.Y = 10;
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.localAI[0] > 90 && NPC.localAI[0] < 120 && NPC.localAI[2] <= 0 && NPC.velocity.Y == 0 && damage > 1000)
            {
                NPC.localAI[2]++;
                NPC.netUpdate = true;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (NPC.localAI[0] <= 60)
            {
                damage = (int)(damage * (NPC.localAI[0] / 60f));
                crit = false;
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (NPC.localAI[0] <= 60)
            {
                damage = (int)(damage * (NPC.localAI[0] / 60f));
                crit = false;
            }
        }
        public override void AI()
        {
            var source = NPC.GetSource_FromAI();
            NPC.localAI[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(false);
                P = Main.player[NPC.target];
                if (!P.active || P.dead)
                {
                    NPC.velocity = new Vector2(0f, -100f);
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
            if (NPC.localAI[0] <= 60)
            {
                if (NPC.localAI[0] == 1)
                {
                    NPC.direction = (int)NPC.ai[0];
                    NPC.ai[0] = 0;
                }
                NPC.scale = 0.625f + (NPC.localAI[0] * 0.00625f);
                NPC.width = (int)(100 * NPC.scale);
                NPC.height = (int)(170 * NPC.scale);
                NPC.position.X -= 0.625f / 2f;
                NPC.position.Y -= 1.0625f;
                NPC.netUpdate = true;
            }
            else
            {
                NPC.width = 118;
                NPC.height = 170;
                NPC.scale = 1;

                if (NPC.Center.Y < 666)
                {
                    NPC.velocity.Y = 6;
                }
                #region Death
                if (NPC.localAI[3] >= 100)
                {
                    NPC.noTileCollide = false;
                    NPC.dontTakeDamage = true;
                    NPC.velocity.X = 0;
                    NPC.localAI[3]++;
                    if (NPC.localAI[2] <= 0)
                    {
                        if (NPC.velocity.Y == 0 || NPC.localAI[3] > 102)
                        {
                            NPC.localAI[3]++;
                            NPC.velocity.Y = 0;
                        }
                        if (NPC.localAI[3] == 110)
                        {
                            SoundEngine.PlaySound(SoundID.NPCDeath10, NPC.Center);
                        }
                    }
                    if (NPC.localAI[3] >= 330f)
                    {
                        NPC.life = 0;
                        NPC.HitEffect(0, 0);
                        NPC.checkDead();
                    }
                    if (NPC.localAI[3] < 102)
                    {
                        NPC.velocity.Y += 0.3f;
                        if (NPC.velocity.Y > 15)
                        {
                            NPC.velocity.Y = 15;
                        }
                    }
                    #endregion
                }
                else
                {
                    NPC.dontTakeDamage = false;
                    if (NPC.localAI[2] <= 0)
                    {
                        if (NPC.Center.X < P.Center.X)
                        {
                            NPC.direction = 1;
                        }
                        else
                        {
                            NPC.direction = -1;
                        }
                    }
                    if (NPC.ai[0] > 0)
                    {
                        NPC.ai[0]++;
                    }
                    if (NPC.ai[1] > 0)
                    {
                        NPC.ai[1]++;
                    }
                    if (NPC.ai[2] > 0)
                    {
                        NPC.ai[2]++;
                    }
                    if (NPC.ai[3] > 0)
                    {
                        NPC.ai[3]++;
                    }
                    if (NPC.ai[0] <= 0 && NPC.ai[1] <= 0 && NPC.ai[2] <= 0 && NPC.ai[3] <= 0 && NPC.localAI[0] > 130 && NPC.localAI[2] <= 0)
                    {
                        if (Main.rand.NextBool(2) && NPC.Distance(P.MountedCenter) < 400)
                        {
                            NPC.ai[0]++;
                            //Acid Spit
                        }
                        else if ((Main.rand.NextBool(3) || NPC.Distance(P.MountedCenter) > 800) && Math.Abs(P.MountedCenter.Y - NPC.Center.Y) < 100)
                        {
                            NPC.ai[2]++;
                            //Dash Punch
                        }
                        else if (Main.rand.NextBool(2) || NPC.Distance(P.MountedCenter) > 800)
                        {
                            NPC.ai[1]++;
                            //Jump
                        }
                        else
                        {
                            NPC.ai[3]++;
                            //Missiles
                        }
                        NPC.netUpdate = true;
                    }

                    #region Jump
                    if (NPC.velocity.Y > 0 && NPC.ai[1] > 30)
                    {
                        NPC.localAI[1] = 1;
                        NPC.noTileCollide = false;
                        if (P.position.Y > NPC.position.Y + NPC.height)
                        {
                            NPC.noTileCollide = true;
                        }
                    }
                    if (NPC.velocity.Y == 0)
                    {
                        if (NPC.localAI[1] > 0)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(source, NPC.Center.X, NPC.Center.Y, 8f, 0, ModContent.ProjectileType<SaxWave>(), 100, 0f, Main.myPlayer);
                                Projectile.NewProjectile(source, NPC.Center.X, NPC.Center.Y, -8f, 0, ModContent.ProjectileType<SaxWave>(), 100, 0f, Main.myPlayer);
                            }
                            SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                            for (int i = 0; i < 60; i++)
                            {
                                int dustType = 1;
                                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
                                Dust dust = Main.dust[dustIndex];
                                dust.velocity.X = dust.velocity.X + Main.rand.Next(-12, 12);
                                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -5);
                            }
                            NPC.localAI[1] = 0;
                            NPC.ai[1] = 0;
                            NPC.frame.Y = 0;
                            NPC.velocity.X = 0;
                            NPC.localAI[0] = 110;
                        }
                    }
                    if (NPC.ai[1] >= 40 && NPC.ai[1] < 100)
                    {
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(P.MountedCenter, NPC.Center) / 30));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, NPC.Center) / 30));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, NPC.Center) / 30));
                        Vector2 jumpos = predictedPos + new Vector2(0f, -500f);
                        if (NPC.velocity.Y == 0)
                        {
                            NPC.velocity = NPC.DirectionTo(jumpos) * 30;
                            NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((jumpos.Y) - (NPC.position.Y + NPC.height)));
                            NPC.frame.Y = 1400;
                            SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_1"), NPC.Center); //214
                        }
                        NPC.noTileCollide = true;
                        if (Math.Abs(NPC.Center.Y - jumpos.Y) < 200)
                        {
                            if (NPC.velocity.X > 0)
                            {
                                NPC.direction = 1;
                                if (NPC.Center.X > P.Center.X)
                                {
                                    NPC.ai[1] = 100;
                                }
                            }
                            else
                            {
                                NPC.direction = -1;
                                if (NPC.Center.X < P.Center.X)
                                {
                                    NPC.ai[1] = 100;
                                }
                            }
                        }
                    }
                    if (NPC.ai[1] == 100)
                    {
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(P.MountedCenter, NPC.Center) / 15));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, NPC.Center) / 15));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, NPC.Center) / 15));
                        NPC.velocity = NPC.DirectionTo(predictedPos) * 15;
                        NPC.damage = (Main.expertMode ? 375 : 250);
                    }
                    if (NPC.ai[1] > 100)
                    {
                        if (P.position.Y <= NPC.position.Y + NPC.height)
                        {
                            NPC.noTileCollide = false;
                        }
                    }
                    NPC.damage = (Main.expertMode ? 188 : 125);
                    NPC.velocity.Y += 0.3f;
                    if (NPC.velocity.Y > 15f)
                    {
                        NPC.velocity.Y = 15f;
                    }

                    #endregion

                    #region Dash Punch
                    if (NPC.ai[2] == 2)
                    {
                        SoundEngine.PlaySound(SoundID.Item13, NPC.Center);
                    }
                    if (NPC.ai[2] == 20)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_ground_miss_2"), NPC.Center); //212
                    }
                    if (NPC.ai[2] == 40)
                    {
                        float vel = 20;
                        if (Math.Abs(P.Center.X - NPC.Center.X) > 800)
                        {
                            Vector2 predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(P.MountedCenter, NPC.Center) / 40));
                            predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, NPC.Center) / 40));
                            predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, NPC.Center) / 40));
                            vel = Math.Abs(predictedPos.X - NPC.Center.X) / 40;
                        }
                        NPC.velocity.X = NPC.direction * vel;
                        Vector2 position = NPC.Center + new Vector2(24 * NPC.direction, -6);
                        Vector2 velocity = new Vector2(10 * NPC.direction, 3);
                        int type = ModContent.ProjectileType<SAXMissile>();
                        int damage = 40;
                        int numberProjectiles = 5;
                        float spread = 90;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                            float scale = 1f - (Main.rand.NextFloat() * .3f);
                            perturbedSpeed = perturbedSpeed * scale;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                            }
                        }
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_3"), NPC.Center); //216
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileShoot"), NPC.Center);
                    }
                    if (NPC.ai[2] > 40 && NPC.ai[2] < 100)
                    {
                        NPC.velocity.Y = 0;
                        NPC.noTileCollide = true;
                        if (NPC.velocity.X > 0)
                        {
                            NPC.direction = 1;
                            if (NPC.position.X > P.position.X + P.width)
                            {
                                NPC.ai[2] = 100;
                                NPC.velocity.X = 0;
                            }
                        }
                        else
                        {
                            NPC.direction = -1;
                            if (NPC.position.X + NPC.width < P.position.X)
                            {
                                NPC.ai[2] = 100;
                                NPC.velocity.X = 0;
                            }
                        }
                        NPC.damage = (Main.expertMode ? 375 : 250);
                    }
                    NPC.damage = (Main.expertMode ? 188 : 125);
                    if (NPC.ai[2] > 100)
                    {
                        NPC.velocity.X = 0;
                        NPC.noTileCollide = false;
                    }
                    if (NPC.ai[2] > 110)
                    {
                        NPC.ai[2] = 0;
                        NPC.localAI[0] = 110;
                    }
                    #endregion

                    #region Missiles
                    if (NPC.ai[3] > 0)
                    {
                        Vector2 position = NPC.Center;
                        Vector2 velocity = Vector2.Zero;
                        int type = ModContent.ProjectileType<SAXMissile>();
                        int damage = 40;
                        int numberProjectiles = 4;
                        float spread = 75;
                        if (NPC.ai[3] == 10)
                        {
                            position = NPC.Center + new Vector2(-50 * NPC.direction, -53);
                            velocity = new Vector2(-10 * NPC.direction, -3);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                                float scale = 1f - (Main.rand.NextFloat() * .3f);
                                perturbedSpeed = perturbedSpeed * scale;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                                }
                            }
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileShoot"), NPC.Center);
                        }
                        if (NPC.ai[3] == 17)
                        {
                            position = NPC.Center + new Vector2(-4 * NPC.direction, -75);
                            velocity = new Vector2(3 * NPC.direction, -10);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                                float scale = 1f - (Main.rand.NextFloat() * .3f);
                                perturbedSpeed = perturbedSpeed * scale;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                                }
                            }
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileShoot"), NPC.Center);
                        }
                        if (NPC.ai[3] == 24)
                        {
                            position = NPC.Center + new Vector2(20 * NPC.direction, -35);
                            velocity = new Vector2(10 * NPC.direction, 3);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                                float scale = 1f - (Main.rand.NextFloat() * .3f);
                                perturbedSpeed = perturbedSpeed * scale;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                                }
                            }
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileShoot"), NPC.Center);
                        }
                        if (NPC.ai[3] > 28)
                        {
                            NPC.ai[3] = 0;
                            NPC.localAI[0] = 120;
                        }
                    }
                    #endregion

                    #region Acid Spit
                    if (NPC.ai[0] == 28)
                    {
                        Vector2 position = NPC.Center + new Vector2(30 * NPC.direction, -9);
                        Vector2 velocity = new Vector2(15 * NPC.direction, -4.5f);
                        int type = ModContent.ProjectileType<SAXSpit>();
                        int damage = 30;
                        int numberProjectiles = 8;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(90));
                            float scale = 1f - (Main.rand.NextFloat() * .3f);
                            perturbedSpeed = perturbedSpeed * scale;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 2, Main.myPlayer);
                            }
                        }
                        position = NPC.Center + new Vector2(-42 * NPC.direction, -70);
                        velocity = new Vector2(-10 * NPC.direction, 3);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, 2, Main.myPlayer);
                        }
                        SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
                        SoundEngine.PlaySound(SoundID.NPCDeath9, NPC.Center);
                    }
                    if (NPC.ai[0] > 35)
                    {
                        NPC.ai[0] = 0;
                        NPC.localAI[0] = 120;
                    }
                    #endregion
                }

                #region Damaged
                if (NPC.localAI[2] > 0)
                {
                    NPC.localAI[2]++;
                    NPC.velocity.X = 0;
                    if (NPC.localAI[2] == 18)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                    }
                    if (NPC.localAI[2] > 54)
                    {
                        NPC.localAI[2] = 0;
                        NPC.localAI[0] = 150;
                    }
                }
                #endregion
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.direction == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }

            Color color = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));

            int xFrameCount = 7;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = new Rectangle(NPC.frame.X, NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vector = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[NPC.type]) / 2f));
            float yOffset = (((1f - NPC.scale) * 12f) + 4f + 31.875f) - ((NPC.scale - 0.625f) * 85);
            Vector2 drawPosition  = new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)(texture.Height / Main.npcFrameCount[NPC.type]) + yOffset + vector.Y);
            if ((NPC.ai[1] >= 100 || NPC.ai[2] >= 40) && NPC.localAI[3] < 100)
            {
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color2 = color * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.5f;
                    Vector2 drawPos = new Vector2(NPC.oldPos[i].X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, NPC.oldPos[i].Y - Main.screenPosition.Y + (float)NPC.height - (float)(texture.Height / Main.npcFrameCount[NPC.type]) + yOffset + vector.Y);
                    spriteBatch.Draw(texture, drawPos, rectangle, color2, NPC.rotation, vector, NPC.scale, effects, 0f);
                }
            }
            spriteBatch.Draw(texture, drawPosition, new Rectangle?(rectangle), color, NPC.rotation, vector, NPC.scale, effects, 0f);
            return false;
        }
        public override bool CheckDead()
        {
            if (NPC.localAI[3] < 100 && NPC.localAI[0] > 60)
            {
                NPC.localAI[3] = 100;
                NPC.localAI[2] = 1;
                NPC.frameCounter = 0;
                NPC.frame.Y = 0;
                NPC.frame.X = 540;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 10;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                NPC.noTileCollide = false;
                return false;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y + 40, Mod.Find<ModNPC>("SAXCoreX").Type);
            }
            //Main.NewText("The SA-X shimmers and reverts to its true form!", 175, 75, 225);

            return true;
        }
    }
}

