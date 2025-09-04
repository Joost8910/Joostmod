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
using JoostMod.Projectiles.Hostile;
using static System.Net.Mime.MediaTypeNames;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class SAX : ModNPC
    {
        private float STATE
        {
            get { return NPC.ai[0]; }
            set { NPC.ai[0] = value; }
        }
        private float AI_Counter
        {
            get { return NPC.ai[1]; }
            set { NPC.ai[1] = value; }
        }
        private float AI_SubState
        {
            get { return NPC.ai[2]; }
            set { NPC.ai[2] = value; }
        }
        private float ARMCANNON_ROT
        {
            get { return NPC.ai[3]; }
            set { NPC.ai[3] = value; }
        }
        private enum StateID : int
        {
            Spawn,
            Search,
            Chase,
            MutantTrans,
            Mutant,
            CoreTrans,
            Core
        }
        private enum Search_Substate : int
        {
            Walking,
            HeadTurn,
            TurnAround,
            MorphBall
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SA-X");
            Main.npcFrameCount[NPC.type] = 20;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedYellow>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 72;
            NPC.damage = 100;
            NPC.defense = 50;
            NPC.lifeMax = 400000;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SAXAppears");
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            NPC.frameCounter = 0;
            NPC.noGravity = true;
        }


        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * balance);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
        public override bool PreKill()
        {
            return false;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)NPC.width);
            writer.Write((int)NPC.height);
            //writer.Write((int)NPC.localAI[0]);
            //writer.Write((int)NPC.localAI[1]);
            //writer.Write((int)NPC.localAI[2]);
            //writer.Write((int)NPC.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.width = reader.ReadInt32();
            NPC.height = reader.ReadInt32();
            //NPC.localAI[0] = reader.ReadInt32();
            //NPC.localAI[1] = reader.ReadInt32();
            //NPC.localAI[2] = reader.ReadInt32();
            //NPC.localAI[3] = reader.ReadInt32();
        }
        public override void FindFrame(int frameHeight)
        {
            int frameWidth = 72;
            if (STATE == (int)StateID.Spawn)
            {
                NPC.frame.Y = 15 * frameHeight;
            }
            if (STATE == (int)StateID.Search)
            {
                if (AI_Counter == 0)
                {
                    NPC.frameCounter = 0;
                }
                if (AI_SubState == (int)Search_Substate.MorphBall)
                {
                    if (AI_Counter < 10 || AI_Counter >= 20)
                    {
                        NPC.frame.X = 3 * frameWidth;
                        if (NPC.velocity.Y == 0)
                            NPC.frame.Y = 15 * frameHeight;
                        else
                            NPC.frame.Y = 14 * frameHeight;
                    }
                    else
                    {
                        NPC.frame.X = frameWidth;
                        NPC.frameCounter++;
                        if (NPC.frameCounter >= 5)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += frameHeight;
                        }
                        if (NPC.frame.Y > 17 * frameHeight)
                        {
                            NPC.frame.Y = 10 * frameHeight;
                        }
                    }
                }
                else
                {
                    NPC.frame.X = 0 * frameWidth;
                    if (NPC.velocity.Y >= 0 && NPC.velocity.Y < 4f)
                    {
                        if (NPC.velocity.X == 0)
                        {
                            if (NPC.frame.Y <= 10 * frameHeight)
                            {
                                NPC.frameCounter = 0;
                                NPC.frame.Y = 10 * frameHeight;
                            }
                            if (AI_SubState == (int)Search_Substate.HeadTurn)
                            {
                                NPC.frame.X = 0;
                                switch ((int)AI_Counter / 10)
                                {
                                    case 0:
                                    case 8:
                                        NPC.frame.Y = 10 * frameHeight;
                                        break;
                                    case 1:
                                    case 7:
                                        NPC.frame.Y = 11 * frameHeight;
                                        break;
                                    case 2:
                                    case 6:
                                        NPC.frame.Y = 12 * frameHeight;
                                        break;
                                    default:
                                        NPC.frame.Y = 13 * frameHeight;
                                        break;
                                }
                            }
                            if (AI_SubState == (int)Search_Substate.TurnAround)
                            {
                                if (AI_Counter < 10 || AI_Counter >= 20)
                                {
                                    NPC.frame.X = 0;
                                    NPC.frame.Y = 14 * frameHeight;
                                }
                                else
                                {
                                    NPC.frame.Y = 15 * frameHeight;
                                }
                            }
                        }
                        else
                        {
                            NPC.frameCounter++;
                            if (NPC.frameCounter >= 6)
                            {
                                NPC.frameCounter = 0;
                                NPC.frame.Y += frameHeight;
                            }
                            if (NPC.frame.Y > 9 * frameHeight)
                            {
                                NPC.frame.Y = 0;
                            }
                        }
                    }
                    else
                    {
                        if (NPC.frame.Y <= 16 * frameHeight)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y = 16 * frameHeight;
                        }
                        NPC.frameCounter++;
                        if (NPC.frameCounter >= 6)
                        {
                            NPC.frame.Y = 17 * frameHeight;
                        }
                    }
                }
            }
        }
        public override void AI()
        {
            if (STATE == (int)StateID.Spawn)
            {
                AI_Counter++;
                if (AI_Counter == 40) // Spawning in
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, Vector2.Zero, ModContent.ProjectileType<DeltaruneExplosion>(), 100, 0); // Placeholder
                    NPC.direction = Main.rand.NextBool(2) ? 1 : -1;
                }
                if (AI_Counter >= 120)
                {
                    AI_Counter = 21;
                    STATE = (int)StateID.Search;
                    AI_SubState = (int)Search_Substate.TurnAround;
                }
            }
            if (STATE == (int)StateID.Search)
            {
                //Main.NewText(AI_SubState);
                int frameHeight = 78;
                if ((NPC.frame.Y == frameHeight * 3 || NPC.frame.Y == frameHeight * 8))
                {
                    if (NPC.soundDelay == 0)
                    {
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SAXFootstep"), NPC.Center);
                        NPC.soundDelay = 10;
                    }
                }
                else
                {
                    NPC.soundDelay = 0;
                }

                ARMCANNON_ROT = 0;
                if (AI_SubState == (int)Search_Substate.Walking)
                {
                    if (AI_Counter == 0)
                    {
                        NPC.velocity.X = 1.5f * NPC.direction;
                    }
                    if (NPC.velocity.Y == 0)
                    {
                        AI_Counter++;
                        if (AI_Counter >= 300)
                        {
                            AI_Counter = 0;
                            AI_SubState = (int)Search_Substate.HeadTurn;
                        }
                        if (NPC.velocity.X == 0)
                        {
                            Vector2 morphCheckPos = new Vector2(NPC.Center.X + (NPC.width / 2 + 16) * NPC.direction, NPC.position.Y + NPC.height - 16) + new Vector2(-16, -16);
                            if (!Collision.SolidCollision(morphCheckPos, 32, 32))
                            {
                                AI_Counter = 0;
                                AI_SubState = (int)Search_Substate.MorphBall;
                            }
                            else
                            {
                                AI_Counter = 0;
                                AI_SubState = (int)Search_Substate.TurnAround;
                            }
                        }
                    }
                }
                if (AI_SubState == (int)Search_Substate.HeadTurn)
                {
                    NPC.velocity.X = 0;
                    AI_Counter++;
                    if (AI_Counter == 30)
                    {
                        if (!Main.rand.NextBool(3))
                        {
                            AI_Counter = 0;
                            AI_SubState = (int)Search_Substate.TurnAround;
                        }
                    }
                    if (AI_Counter >= 90)
                    {
                        AI_Counter = 0;
                        AI_SubState = (int)Search_Substate.Walking;
                    }
                }
                if (AI_SubState == (int)Search_Substate.TurnAround)
                {
                    AI_Counter++;
                    if (AI_Counter == 20)
                    {
                        NPC.direction *= -1;
                    }
                    if (AI_Counter >= 30)
                    {
                        AI_Counter = 0;
                        AI_SubState = (int)Search_Substate.Walking;
                    }
                }
                if (AI_SubState == (int)Search_Substate.MorphBall)
                {
                    //Main.NewText(AI_Counter);
                    if (AI_Counter < 10) // Crouching
                    {
                        if (NPC.velocity.Y == 0)
                        {
                            if (AI_Counter == 0)
                            {
                                NPC.position.Y += (NPC.height - 58);
                            }
                            NPC.height = 58;
                        }
                        else
                        {
                            ARMCANNON_ROT = MathHelper.PiOver2 * NPC.direction;
                        }
                        AI_Counter++;
                    }
                    else if (AI_Counter < 20) // Morphball
                    {
                        if (AI_Counter == 10)
                        {
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MorphBall"), NPC.Center);
                            NPC.position.Y += (NPC.height - 32);
                            NPC.velocity.X = 2.5f * NPC.direction;
                        }

                        if (AI_Counter < 15)
                            AI_Counter++;
                        NPC.height = 32;
                        if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                        {
                            NPC.direction *= -1;
                            NPC.velocity.X = 2 * NPC.direction;
                        }
                        if (NPC.velocity.Y == 0 && AI_Counter >= 15)
                        {
                            Vector2 unMorphCheckPos = new Vector2(NPC.position.X, NPC.position.Y - (72 - NPC.height));
                            if (!Collision.SolidCollision(unMorphCheckPos, 32, 72))
                            {
                                AI_Counter = 20;
                            }
                        }
                    }
                    if (AI_Counter >= 20) //Unmorph
                    {
                        AI_Counter++;
                        if (AI_Counter <= 30)
                        {
                            if (NPC.velocity.Y == 0)
                            {
                                if (AI_Counter == 21)
                                {
                                    NPC.position.Y -= (58 - NPC.height);
                                }
                                NPC.velocity.X = 0;
                                NPC.height = 58;
                            }
                            else
                            {
                                if (AI_Counter == 21)
                                {
                                    NPC.position.Y -= (72 - NPC.height);
                                }
                                NPC.height = 72;
                                ARMCANNON_ROT = MathHelper.PiOver2 * NPC.direction;
                            }
                        }
                        else
                        {
                            AI_Counter = 0;
                            AI_SubState = (int)Search_Substate.Walking;

                            NPC.position.Y -= (72 - NPC.height);
                            NPC.height = 72;
                            NPC.velocity.X = 1.5f * NPC.direction;
                        }
                    }
                }

                NPC.velocity.Y += NPC.gravity;
            }

            if (STATE == (int)StateID.Chase)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SAXChase");
            }
        }
        /* AI
        public override void AI()
        {
            if (NPC.localAI[3] <= 0)
            {
                if (NPC.Center.Y < 666)
                {
                    NPC.velocity.Y = 6;
                }
                Player P = Main.player[NPC.target];
                if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                {
                    NPC.TargetClosest(false);
                    P = Main.player[NPC.target];
                    if (!P.active || P.dead)
                    {
                        NPC.velocity = new Vector2(0f, -100f);
                        NPC.active = false;
                    }
                }
                if (NPC.frame.Y < 312 || (NPC.frame.Y > 312 && NPC.frame.Y < 702))
                {
                    NPC.soundDelay = 0;
                }
                if ((NPC.frame.Y == 312 || NPC.frame.Y == 702) && NPC.soundDelay <= 0 && NPC.ai[1] <= 0 && NPC.localAI[1] <= 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SAXFootstep").WithVolumeScale(0.6f).WithPitchOffset(0.2f), NPC.Center);
                    NPC.soundDelay = 1;
                }
                int yOff = -4;
                if (NPC.frame.Y >= 0)
                {
                    yOff -= 2;
                }
                if (NPC.frame.Y >= 78)
                {
                    yOff -= 2;
                }
                if (NPC.frame.Y >= 156)
                {
                    yOff += 2;
                }
                if (NPC.frame.Y >= 234)
                {
                    yOff += 2;
                }
                if (NPC.frame.Y >= 390)
                {
                    yOff -= 2;
                }
                if (NPC.frame.Y >= 624)
                {
                    yOff += 2;
                }
                Vector2 gunCenter = NPC.Center + new Vector2(NPC.scale * NPC.direction * -2, NPC.scale * yOff);
                if (NPC.ai[0] <= 30)
                {
                    if (NPC.Center.X < P.Center.X)
                    {
                        NPC.direction = 1;
                    }
                    else
                    {
                        NPC.direction = -1;
                    }
                    PredictiveAim(24f, gunCenter);
                }
                NPC.spriteDirection = NPC.direction;
                if (NPC.velocity.X == 0)
                {
                    NPC.ai[1] = 0;
                }
                if (NPC.position.Y > P.Center.Y + 250 && NPC.ai[1] <= 0 && NPC.velocity.Y == 0 && ((NPC.ai[0] > 40 && NPC.ai[2] < 10) || (NPC.ai[0] > 70 && NPC.ai[2] >= 10 && NPC.ai[2] < 12) || (NPC.ai[0] > 145 && NPC.ai[2] >= 12)))
                {
                    Vector2 predictedPos = P.MountedCenter + P.velocity + (P.velocity * (NPC.Distance(P.MountedCenter) / 24));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (NPC.Distance(predictedPos) / 24));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (NPC.Distance(predictedPos) / 24));
                    if (P.velocity.X * NPC.direction <= 0)
                    {
                        Vector2 vel = NPC.DirectionTo(predictedPos) * (NPC.Distance(predictedPos) / 24);
                        NPC.velocity.X = vel.X;
                    }
                    else
                    {
                        NPC.velocity.X = (7 * NPC.direction) + P.velocity.X;
                    }
                    NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((P.MountedCenter.Y + P.velocity.Y) - (NPC.position.Y + NPC.height)));
                    NPC.frame.Y = 702;
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 1;
                    NPC.ai[2] = Main.rand.Next(13);
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SAXJump"), NPC.Center);
                }
                if (NPC.velocity.Y == 0)
                {
                    if (NPC.velocity.X == 0 && NPC.oldVelocity.X != 0 && NPC.ai[1] <= 0)
                    {
                        NPC.velocity.Y = -9f;
                        NPC.velocity.X = 6f * NPC.direction;
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SAXJump"), NPC.Center);
                    }
                    NPC.ai[1] = 0;
                }
                if (NPC.ai[1] > 250)
                {
                    NPC.ai[1] = 0;
                }
                if (P.position.Y > NPC.position.Y + NPC.height && NPC.localAI[1] <= 0)
                {
                    NPC.position.Y++;
                }
                NPC.netUpdate = true;
                if (NPC.ai[1] <= 0)
                {
                    NPC.ai[0]++;
                    if (Math.Abs(NPC.Center.X - P.Center.X) > 80)
                    {
                        if (NPC.velocity.Y == 0)
                        {
                            if ((NPC.velocity.X < 7 && NPC.direction > 0) || (NPC.velocity.X > -7 && NPC.direction < 0))
                            {
                                NPC.velocity.X += 0.5f * NPC.direction;
                            }
                        }
                        else
                        {
                            if (NPC.velocity.X < 7 && NPC.velocity.X > -7)
                            {
                                NPC.velocity.X += 0.5f * NPC.direction;
                            }
                        }
                    }
                    else if (NPC.velocity.Y == 0)
                    {
                        NPC.velocity.X = 0;
                        if (NPC.ai[0] == 10 && NPC.localAI[1] <= 0)
                        {
                            NPC.localAI[1]++;
                        }
                    }
                    if (Math.Abs(NPC.Center.X - P.Center.X) > 300)
                    {
                        if (NPC.Center.X < P.Center.X)
                        {
                            NPC.direction = 1;
                        }
                        else
                        {
                            NPC.direction = -1;
                        }
                        //npc.velocity.X = (7 * npc.direction) + (P.velocity.X * npc.direction > 0 ? P.velocity.X : 0);
                        NPC.velocity.X = (7 * NPC.direction) * (Math.Abs(NPC.Center.X - P.Center.X) / 300);
                    }
                }
                if (NPC.ai[0] >= 30)
                {
                    NPC.ai[1] = 0;
                    if (P.HasBuff(BuffID.Frozen) && NPC.ai[0] == 30)
                    {
                        NPC.ai[2] = 10;
                    }
                    if (NPC.ai[2] < 10)
                    {
                        float vel = 12f;
                        if (NPC.velocity.Length() > vel)
                        {
                            vel += (NPC.velocity.Length() - vel) / 2;
                        }
                        if (NPC.ai[0] < 30)
                        {
                            PredictiveAim(vel * 2, gunCenter);
                        }
                        if (NPC.ai[0] == 30)
                        {
                            NPC.ai[0]++;
                            int damage = 50;
                            int type = ModContent.ProjectileType<SAXBeam>();
                            float rotation = NPC.ai[3];
                            Vector2 aim = new Vector2((float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1));
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceBeam"), NPC.Center);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), gunCenter + (aim * 10 * NPC.scale), aim * vel, type, damage, 0f, Main.myPlayer, 1);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), gunCenter + (aim * 10 * NPC.scale), aim * vel, type, damage, 0f, Main.myPlayer);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), gunCenter + (aim * 10 * NPC.scale), aim * vel, type, damage, 0f, Main.myPlayer, -1);
                            }
                        }
                        if (NPC.ai[0] > 45)
                        {
                            NPC.ai[0] = 0;
                            NPC.ai[2] += 1 + Main.rand.Next(4);
                            NPC.netUpdate = true;
                        }
                    }
                    else if (NPC.ai[2] >= 10 && NPC.ai[2] < 12)
                    {
                        float vel = 14f;
                        if (NPC.velocity.Length() > vel)
                        {
                            vel += (NPC.velocity.Length() - vel) / 2;
                        }
                        if (NPC.ai[0] < 60)
                        {
                            PredictiveAim(vel * 2, gunCenter);
                        }
                        if (NPC.ai[0] == 30)
                        {
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileClick"), NPC.Center);
                            NPC.ai[0]++;
                        }
                        if (NPC.ai[0] == 60)
                        {
                            NPC.ai[0]++;
                            int damage = 75;
                            int type = ModContent.ProjectileType<SAXSuperMissile>();
                            float rotation = NPC.ai[3];
                            Vector2 aim = new Vector2((float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1));
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SuperMissileShoot"), NPC.Center);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), gunCenter + (aim * 26 * NPC.scale), aim * vel, type, damage, 10, Main.myPlayer);
                            }
                        }
                        if (NPC.ai[0] > 75)
                        {
                            NPC.ai[0] = 0;
                            NPC.ai[2] = 0;
                        }
                    }
                    else
                    {
                        float vel = 12f;
                        if (NPC.velocity.Length() > vel)
                        {
                            vel += (NPC.velocity.Length() - vel) / 2;
                        }
                        if (NPC.ai[0] < 120)
                        {
                            PredictiveAim(vel * 2, gunCenter);
                        }
                        if (NPC.ai[0] == 30)
                        {
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ChargingStart"), NPC.Center);
                            NPC.ai[0]++;
                            NPC.localAI[0] = 0;
                        }
                        if (NPC.ai[0] == 55)
                        {
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ChargingStart2"), NPC.Center);
                            NPC.ai[0]++;
                        }
                        if (NPC.ai[0] >= 80 && NPC.ai[0] % 15 == 5 && NPC.ai[0] < 120)
                        {
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ChargingLoop"), NPC.Center);
                        }
                        if ((NPC.ai[0] % 4) == 0 && NPC.ai[0] < 78)
                        {
                            NPC.localAI[0] = (NPC.localAI[0] + 1) % 12;
                        }
                        if (NPC.ai[0] >= 78 && (NPC.ai[0] % 4) == 0)
                        {
                            NPC.localAI[0] = (NPC.localAI[0] % 3) + 10;
                        }
                        if (NPC.ai[0] == 120)
                        {
                            NPC.ai[0]++;
                            int damage = 100;
                            int type = ModContent.ProjectileType<SAXBeamCharged>();
                            float rotation = NPC.ai[3];
                            Vector2 aim = new Vector2((float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1));
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceBeamCharged"), NPC.Center);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), gunCenter + (aim * 10 * NPC.scale), aim * vel, type, damage, 0f, Main.myPlayer, 1);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), gunCenter + (aim * 10 * NPC.scale), aim * vel, type, damage, 0f, Main.myPlayer);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), gunCenter + (aim * 10 * NPC.scale), aim * vel, type, damage, 0f, Main.myPlayer, -1);
                            }
                        }
                        if (NPC.ai[0] > 150)
                        {
                            NPC.ai[0] = 0;
                            NPC.ai[2] = 0;
                        }
                    }
                    if (NPC.ai[0] > 150)
                    {
                        NPC.ai[0] = 0;
                        NPC.ai[2] = 0;
                    }
                }
                if (NPC.localAI[1] >= 1)
                {
                    if (NPC.localAI[1] == 1)
                    {
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MorphBall"), NPC.Center);
                        NPC.position.Y += 44;
                    }
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 0;
                    NPC.localAI[1]++;
                    NPC.velocity.X = 0;
                    if (NPC.velocity.Y < 0)
                    {
                        NPC.velocity.Y = 0;
                    }
                    NPC.height = 32;
                    NPC.width = 32;
                    if (NPC.localAI[1] == 30)
                    {
                        int damage = 150;
                        int type = ModContent.ProjectileType<SAXPowerBomb>();
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/LayBomb"), NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0, 0, type, damage, 0f, Main.myPlayer);
                        }
                    }
                    if (NPC.localAI[1] > 250)
                    {
                        NPC.localAI[1] = -5;
                        NPC.width = 36;
                        NPC.height = 78;
                        NPC.position.Y -= 50;
                    }
                    NPC.netUpdate = true;
                }
                if (NPC.ai[1] >= 1)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[1]++;
                    if (NPC.velocity.Y > 0 && NPC.position.Y > P.MountedCenter.Y + 30 && NPC.Distance(P.MountedCenter) < 900)
                    {
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (P.velocity * (NPC.Distance(P.MountedCenter) / 24));
                        predictedPos = P.MountedCenter + P.velocity + (P.velocity * (NPC.Distance(predictedPos) / 24));
                        predictedPos = P.MountedCenter + P.velocity + (P.velocity * (NPC.Distance(predictedPos) / 24));
                        if (P.velocity.X * NPC.direction <= 0)
                        {
                            Vector2 vel = NPC.DirectionTo(predictedPos) * (NPC.Distance(predictedPos) / 24);
                            NPC.velocity.X = vel.X;
                        }
                        else
                        {
                            NPC.velocity.X = (7 * NPC.direction) + P.velocity.X;
                        }
                        if (NPC.ai[1] < 150)
                        {
                            NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((P.MountedCenter.Y + P.velocity.Y) - (NPC.position.Y + NPC.height)));
                        }
                    }
                    if (NPC.ai[1] < 150 && ((NPC.Center.X > P.MountedCenter.X + 300 && NPC.velocity.X > 0) || (NPC.Center.X < P.MountedCenter.X - 300 && NPC.velocity.X < 0)))
                    {
                        NPC.velocity.X = (7 * NPC.direction);
                    }
                    //Main.PlaySound(2, npc.Center, 7);
                    if (!Collision.CanHitLine(new Vector2(NPC.Center.X, NPC.position.Y), 1, 1, P.position, P.width, P.height) && NPC.velocity.Y < 0)
                    {
                        NPC.noTileCollide = true;
                    }
                    else
                    {
                        NPC.noTileCollide = false;
                    }
                    NPC.damage = Main.expertMode ? 300 : 200;
                }
                else
                {
                    NPC.damage = Main.expertMode ? 150 : 100;
                }
                if ((NPC.ai[1] % 15) == 1)
                {
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ScrewAttack").WithPitchOffset(-0.1f), NPC.Center); 
                }
            } 
            if (NPC.localAI[3] > 0)
            {
                NPC.dontTakeDamage = true;
                NPC.velocity.X = 0;
                if (NPC.velocity.Y == 0 || NPC.localAI[3] > 2)
                {
                    NPC.localAI[3]++;
                    NPC.velocity.Y = 0;
                }
                if (NPC.localAI[3] >= 198f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
            }
            if (NPC.localAI[3] < 2)
            {
                NPC.velocity.Y += 0.3f;
                if (NPC.velocity.Y > 10)
                {
                    NPC.velocity.Y = 10;
                }
            }
        }
        */
        private void PredictiveAim(float speed, Vector2 origin)
        {
            Player P = Main.player[NPC.target];
            Vector2 vel = (Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200 ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            if (predictedPos.X < NPC.Center.X && NPC.velocity.Y == 0)
            {
                if (NPC.direction > 0)
                {
                    NPC.velocity.X = -1;
                }
                NPC.direction = -1;
            }
            if (predictedPos.X > NPC.Center.X && NPC.velocity.Y == 0)
            {
                if (NPC.direction < 0)
                {
                    NPC.velocity.X = 1;
                }
                NPC.direction = 1;
            }
            NPC.ai[3] = (float)Math.Atan2(origin.Y - predictedPos.Y, origin.X - predictedPos.X);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (STATE == (int)StateID.Spawn && AI_Counter < 60) // Hide before spawn explosion
            {
                return false;
            }
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.direction == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            if (STATE == (int)StateID.Spawn || STATE == (int)StateID.Search || STATE == (int)StateID.Chase)
            {
                Texture2D texture = TextureAssets.Npc[NPC.type].Value;
                Texture2D visorTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Visor");
                int xFrameCount = 4;
                int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
                int frameWidth = texture.Width / xFrameCount;
                Rectangle rectangle = new Rectangle(NPC.frame.X, NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
                Vector2 origin = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[NPC.type]) / 2f));

                Color lightColor = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));

                if (!(NPC.frame.X == 0 && NPC.frame.Y >= 14 * frameHeight && NPC.frame.Y <= 17 * frameHeight) &&                // Turnaround and falling
                    !(NPC.frame.X == 4 * frameWidth && NPC.frame.Y == 17) &&                                                    // Shinespark
                    !(NPC.frame.X == 1 * frameWidth && NPC.frame.Y >= 10 * frameHeight && NPC.frame.Y <= 17 * frameHeight) &&   // Morphball
                    !(NPC.frame.X == 2 * frameWidth && NPC.frame.Y >= 10 * frameHeight && NPC.frame.Y <= 17 * frameHeight))     // Screw Attack
                {
                    Texture2D cannonTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_ArmCannon");
                    Rectangle cannonRect = new Rectangle(0, 0, (cannonTex.Width), (cannonTex.Height / 2));
                    Vector2 cannonOrigin = new Vector2((float)cannonTex.Width / 2, (float)cannonTex.Height / 4);
                    float rotation = ARMCANNON_ROT;

                    int xOff = -2;
                    int yOff = -4;
                    switch (NPC.frame.Y / frameHeight)
                    {
                        case 1:
                        case 2:
                        case 6:
                        case 7:
                            yOff -= 2;
                            break;
                    }
                    if (NPC.frame.X == 3 * frameWidth && NPC.frame.Y == 15 * frameHeight)
                    {
                        yOff = 4;
                    }
                    if (NPC.frame.X == 3 * frameWidth && NPC.frame.Y == 14 * frameHeight)
                    {
                        yOff -= 23;
                        xOff += 7;
                    }
                    spriteBatch.Draw(cannonTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * xOff, NPC.scale * yOff), new Rectangle?(cannonRect), lightColor, rotation, cannonOrigin, NPC.scale, effects, 0f);
                }
                spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + origin.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - frameHeight + 4f + origin.Y), new Rectangle?(rectangle), lightColor, NPC.rotation, origin, NPC.scale, effects, 0f);
                spriteBatch.Draw(visorTex, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + origin.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - frameHeight + 4f + origin.Y), new Rectangle?(rectangle), Color.White, NPC.rotation, origin, NPC.scale, effects, 0f);
            }
            return false;
        }

        /* ModifyHitBy
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (NPC.ai[1] >= 1) // Screw Attacking
            {
                modifiers.FinalDamage /= 2;
                modifiers.DisableCrit();
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (NPC.ai[1] >= 1)
            {
                modifiers.FinalDamage /= 2;
                modifiers.DisableCrit();
            }
        }
        */
        /* OnHitPlayer
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (NPC.ai[1] >= 1)
            {
                NPC.velocity.X = (-7 * NPC.direction) + target.velocity.X;
                NPC.ai[1] = 200;
                if (Main.expertMode)
                {
                    target.wingTime = 0;
                    target.rocketTime = 0;
                    target.mount.Dismount(target);
                    target.velocity.Y = 10;
                    NPC.velocity.Y = 10;
                }
            }
        }
        */
        /* CanHit
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if ((Math.Abs(target.MountedCenter.Y - NPC.Center.Y) < (target.height / 2) + 25 && Math.Abs(target.MountedCenter.X - NPC.Center.X) < (target.width / 2) + 25) && NPC.localAI[1] <= 0)
            {
                return base.CanHitPlayer(target, ref cooldownSlot);
            }
            return false;
        }
        public override bool CanHitNPC(NPC target)
        {
            if ((Math.Abs(target.Center.Y - NPC.Center.Y) < (target.height / 2) + 25 && Math.Abs(target.Center.X - NPC.Center.X) < (target.width / 2) + 25) && NPC.localAI[1] <= 0)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        */
        /* FindFrame
        public override void FindFrame(int frameHeight)
        {
            NPC.frame.X = 0;
            if (NPC.localAI[3] > 0)
            {
                if (NPC.localAI[3] < 60)
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter > 15)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += 126;
                        if (NPC.frame.Y > 126)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                }
                else if (NPC.localAI[3] < 75)
                {
                    NPC.frame.Y = 2 * 126;
                }
                else if (NPC.localAI[3] < 90)
                {
                    NPC.frame.Y = 3 * 126;
                }
                else if (NPC.localAI[3] < 96)
                {
                    NPC.frame.Y = 4 * 126;
                }
                else if (NPC.localAI[3] < 102)
                {
                    NPC.frame.Y = 3 * 126;
                }
                else if (NPC.localAI[3] < 108)
                {
                    NPC.frame.Y = 4 * 126;
                }
                else if (NPC.localAI[3] < 114)
                {
                    NPC.frame.Y = 5 * 126;
                }
                else if (NPC.localAI[3] < 120)
                {
                    NPC.frame.Y = 4 * 126;
                }
                else if (NPC.localAI[3] < 126)
                {
                    NPC.frame.Y = 5 * 126;
                }
                else if (NPC.localAI[3] < 132)
                {
                    NPC.frame.Y = 6 * 126;
                }
                else if (NPC.localAI[3] < 138)
                {
                    NPC.frame.Y = 5 * 126;
                }
                else if (NPC.localAI[3] < 144)
                {
                    NPC.frame.Y = 6 * 126;
                }
                else if (NPC.localAI[3] < 150)
                {
                    NPC.frame.Y = 7 * 126;
                }
                else if (NPC.localAI[3] < 156)
                {
                    NPC.frame.Y = 6 * 126;
                }
                else if (NPC.localAI[3] < 162)
                {
                    NPC.frame.Y = 7 * 126;
                }
                else if (NPC.localAI[3] < 168)
                {
                    NPC.frame.Y = 8 * 126;
                }
                else if (NPC.localAI[3] < 174)
                {
                    NPC.frame.Y = 7 * 126;
                }
                else if (NPC.localAI[3] < 180)
                {
                    NPC.frame.Y = 8 * 126;
                }
                else if (NPC.localAI[3] < 186)
                {
                    NPC.frame.Y = 9 * 126;
                }
                else if (NPC.localAI[3] < 192)
                {
                    NPC.frame.Y = 8 * 126;
                }
                else if (NPC.localAI[3] < 198)
                {
                    NPC.frame.Y = 9 * 126;
                }
            }
            else
            {
                if (NPC.ai[1] <= 0 && NPC.localAI[1] <= 0)
                {
                    if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                    {
                        NPC.frameCounter = 0;
                        if ((NPC.ai[0] >= 30 && NPC.ai[2] < 10) || (NPC.ai[0] >= 60 && NPC.ai[2] >= 10 && NPC.ai[2] < 12) || (NPC.ai[0] >= 120 && NPC.ai[2] >= 12))
                        {
                            NPC.frame.Y = 1482;
                        }
                        else
                        {
                            NPC.frame.Y = 1404;
                        }
                        if (NPC.ai[3] > 25 * (float)(Math.PI / 180) && NPC.ai[3] < 155 * (float)(Math.PI / 180))
                        {
                            NPC.frame.X = 72;
                        }
                        if (NPC.ai[3] > 70 * (float)(Math.PI / 180) && NPC.ai[3] < 110 * (float)(Math.PI / 180))
                        {
                            NPC.frame.X = 144;
                        }
                    }
                    else
                    {
                        if (NPC.velocity.Y == 0)
                        {
                            NPC.frameCounter += Math.Abs(NPC.velocity.X);
                            if (NPC.frameCounter >= 10)
                            {
                                NPC.frameCounter = 0;
                                NPC.frame.Y = (NPC.frame.Y + 78);
                            }
                            if (NPC.frame.Y >= 780)
                            {
                                NPC.frame.Y = 0;
                            }
                            if (NPC.ai[3] > 25 * (float)(Math.PI / 180) && NPC.ai[3] < 155 * (float)(Math.PI / 180))
                            {
                                NPC.frame.X = 72;
                            }
                            if (NPC.ai[3] > 70 * (float)(Math.PI / 180) && NPC.ai[3] < 110 * (float)(Math.PI / 180))
                            {
                                NPC.frame.X = 144;
                            }
                        }
                        else
                        {
                            NPC.frame.X = 0;
                            NPC.frame.Y = 1560;
                            if (NPC.ai[3] < -25 * (float)(Math.PI / 180) && NPC.ai[3] > -155 * (float)(Math.PI / 180))
                            {
                                NPC.frame.X = 72;
                                NPC.frame.Y = 1638;
                            }
                            if (NPC.ai[3] < -70 * (float)(Math.PI / 180) && NPC.ai[3] > -110 * (float)(Math.PI / 180))
                            {
                                NPC.frame.X = 0;
                                NPC.frame.Y = 1638;
                            }
                            if (NPC.ai[3] > 25 * (float)(Math.PI / 180) && NPC.ai[3] < 155 * (float)(Math.PI / 180))
                            {
                                NPC.frame.X = 72;
                                NPC.frame.Y = 1560;
                            }
                            if (NPC.ai[3] > 60 * (float)(Math.PI / 180) && NPC.ai[3] < 120 * (float)(Math.PI / 180))
                            {
                                NPC.frame.X = 144;
                                NPC.frame.Y = 1560;
                            }
                        }
                    }
                }
                else if (NPC.localAI[1] <= 0)
                {
                    NPC.frame.X = 0;
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= 6)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = (NPC.frame.Y + 78);
                    }
                    if (NPC.frame.Y >= 1404)
                    {
                        NPC.frame.Y = 780;
                    }
                }
                else
                {
                    NPC.frame.X = 144;
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= 6)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = (NPC.frame.Y + 78);
                    }
                    if (NPC.frame.Y >= 1404)
                    {
                        NPC.frame.Y = 780;
                    }
                }
            }
        }
        */
        /* PreDraw
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
            //Color alpha2 = npc.GetAlpha(buffColor);
            //Color color = new Color((int)((float)npc.color.R * ((float)alpha2.R / 255)), (int)((float)npc.color.G * ((float)alpha2.G / 255)), (int)((float)npc.color.B * ((float)alpha2.B / 255)));
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_ArmCannon");
            Rectangle rect = new Rectangle(0, 0, (tex.Width), (tex.Height / 2));
            Vector2 vect = new Vector2((float)tex.Width / 2, (float)tex.Height / 4);
            float rotation = NPC.direction > 0 ? NPC.ai[3] - (float)(Math.PI) : NPC.ai[3];

            Texture2D tex2 = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_ChargingBeam");
            Rectangle rect2 = new Rectangle(0, (int)NPC.localAI[0] * 46, (tex2.Width), (tex2.Height / 13));
            Vector2 vect2 = new Vector2((float)tex2.Width / 2, (float)tex2.Height / 26);

            int yOff = -4;
            if (NPC.frame.Y >= 0)
            {
                yOff -= 2;
            }
            if (NPC.frame.Y >= 78)
            {
                yOff -= 2;
            }
            if (NPC.frame.Y >= 156)
            {
                yOff += 2;
            }
            if (NPC.frame.Y >= 234)
            {
                yOff += 2;
            }
            if (NPC.frame.Y >= 390)
            {
                yOff -= 2;
            }
            if (NPC.frame.Y >= 624)
            {
                yOff += 2;
            }
            int xOff = -2;
            if ((NPC.ai[0] >= 30 && NPC.ai[2] < 10) || (NPC.ai[0] >= 60 && NPC.ai[2] >= 10 && NPC.ai[2] < 12) || (NPC.ai[0] >= 120 && NPC.ai[2] >= 12))
            {
                if (NPC.frame.X < 144)
                {
                    xOff = -6;
                }
                else
                {
                    yOff += 2;
                }
                if (NPC.velocity.X == 0)
                {
                    yOff += 2;
                }
            }
            if (NPC.ai[3] > 25 * (float)(Math.PI / 180) && NPC.ai[3] < 155 * (float)(Math.PI / 180))
            {
                xOff += 6;
                yOff -= 6;
            }
            if (NPC.frame.X >= 144)
            {
                yOff -= 10;
            }
            if (NPC.ai[2] >= 10 && NPC.ai[2] < 12 && NPC.ai[0] >= 30)
            {
                rect = new Rectangle(0, (tex.Height / 2), (tex.Width), (tex.Height / 2));
            }
            if (NPC.ai[1] <= 0 && NPC.localAI[1] <= 0 && NPC.localAI[3] <= 0)
            {
                spriteBatch.Draw(tex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * xOff, NPC.scale * yOff), new Rectangle?(rect), color, rotation, vect, NPC.scale, effects, 0f);
            }
            if (NPC.ai[2] >= 12 && NPC.ai[0] >= 30 && NPC.ai[0] < 120 && NPC.localAI[3] <= 0)
            {
                spriteBatch.Draw(tex2, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * xOff, NPC.scale * yOff), new Rectangle?(rect2), Color.White, rotation, vect2, NPC.scale, effects, 0f);
            }


            int xFrameCount = 3;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = new Rectangle(NPC.frame.X, NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vector = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[NPC.type]) / 2f));
            if (NPC.localAI[3] > 0)
            {
                Texture2D tex3 = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Death");
                Rectangle rect3 = new Rectangle(0, NPC.frame.Y, tex3.Width, (tex3.Height / 10));
                Vector2 vect3 = new Vector2((tex3.Width / 2f), ((tex3.Height / 10) / 2f));
                spriteBatch.Draw(tex3, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(tex3.Width) / 2f + vect3.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)(tex3.Height / 10) + 4f + vect3.Y), new Rectangle?(rect3), color, NPC.rotation, vect3, NPC.scale, effects, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)(texture.Height / Main.npcFrameCount[NPC.type]) + 4f + vector.Y), new Rectangle?(rectangle), color, NPC.rotation, vector, NPC.scale, effects, 0f);
            }
            return false;
        }
        */
        /* CheckDead
        public override bool CheckDead()
        {
            //Main.NewText("The SA-X enlarges and mutates!", 175, 75, 225);
            if (NPC.localAI[3] <= 0)
            {
                NPC.localAI[3] = 1;
                NPC.frame.Y = 0;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 10;
                if (NPC.height == 32)
                {
                    NPC.position.Y -= 50;
                }
                NPC.width = 36;
                NPC.height = 78;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y+38, ModContent.NPCType<SAXMutant>(), 0, NPC.direction);
            }
            return true;
        }
        */
    }
}

