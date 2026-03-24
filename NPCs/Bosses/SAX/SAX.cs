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
using JoostMod.Dusts;
using Terraria.Graphics.Shaders;

namespace JoostMod.NPCs.Bosses.SAX
{
    [AutoloadBossHead]
    public class SAX : ModNPC
    {
        private ref float State => ref NPC.ai[0];
        private ref float AI_Counter => ref NPC.ai[1];
        private ref float AI_SubState => ref NPC.ai[2];
        private ref float Aiming_Rotation => ref NPC.ai[3];
        private ref float TransShaderIntensity => ref NPC.localAI[0];

        private int Unstuck_Check = 0;
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
            MorphBall,
            ScrewAttack,
            XRayScope,
            UnstuckTransform
        }
        const int BASE_WIDTH = 28;
        const int BASE_HEIGHT = 62;
        const int CROUCH_HEIGHT = 52;
        const int BALL_HEIGHT = 28;
        const float WALK_SPEED = 1.5f;

        private static int mutantHeadSlot = -1;
        private static int coreHeadSlot = -1;
        public override void Load()
        {
            string texMutant = BossHeadTexture + "_Mutant";
            string texCore = BossHeadTexture + "_Core";
            mutantHeadSlot = Mod.AddBossHeadTexture(texMutant);
            coreHeadSlot = Mod.AddBossHeadTexture(texCore);
        }
        public override void BossHeadSlot(ref int index)
        {
            int slotM = mutantHeadSlot;
            int slotC = coreHeadSlot;
            if (State == (int)StateID.Mutant)
            {
                index = slotM;
            }
            else if (State == (int)StateID.Core)
            {
                index = slotC;
            }
            else if (State != (int)StateID.Chase)
            {
                index = -1;
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SA-X");
            Main.npcFrameCount[NPC.type] = 21;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedYellow>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedGreen>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedRed>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedBlue>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = BASE_WIDTH;
            NPC.height = BASE_HEIGHT;
            NPC.damage = 100;
            NPC.defense = 100;
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
            NPC.GravityIgnoresLiquid = true;
            NPC.GravityIgnoresSpace = true;
            NPC.dontTakeDamage = true;
            NPC.waterMovementSpeed = 1f;
            NPC.honeyMovementSpeed = 1f;
            NPC.lavaMovementSpeed = 1f;
            NPC.shimmerMovementSpeed = 1f;
        }


        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * balance);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
        //public override bool PreKill()
        //{
        //    return false;
        //}
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(NPC.width);
            writer.Write(NPC.height);
            writer.Write(Unstuck_Check);
            //writer.Write((int)NPC.localAI[0]);
            //writer.Write((int)NPC.localAI[1]);
            //writer.Write((int)NPC.localAI[2]);
            //writer.Write((int)NPC.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.width = reader.ReadInt32();
            NPC.height = reader.ReadInt32();
            Unstuck_Check = reader.ReadInt32();
            //NPC.localAI[0] = reader.ReadInt32();
            //NPC.localAI[1] = reader.ReadInt32();
            //NPC.localAI[2] = reader.ReadInt32();
            //NPC.localAI[3] = reader.ReadInt32();
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.dontTakeDamage)
            {
                return false;
            }
            return true;
        }
        public override bool CanHitNPC(NPC target)
        {
            if (NPC.dontTakeDamage)
            {
                return false;
            }
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            int frameWidth = 72;
            if (State == (int)StateID.Spawn)
            {
                NPC.frame.Y = 15 * frameHeight;
            }
            if (State == (int)StateID.Search)
            {
                if (AI_Counter == 0)
                {
                    NPC.frameCounter = 0;
                }
                if (AI_SubState == (int)Search_Substate.UnstuckTransform)
                {
                    if (AI_Counter < 120 || AI_Counter >= 420)
                    {
                        if (AI_Counter >= 420)
                        {
                            NPC.frame.Y = 16 * frameHeight;
                            NPC.frame.X = 0;
                        }
                        else
                        {
                            if (AI_Counter < 40)
                            {
                                if (NPC.velocity.Y == 0)
                                {
                                    if (AI_Counter > 30)
                                    {
                                        NPC.frame.X = 3 * frameWidth;
                                        NPC.frame.Y = 15 * frameHeight;
                                    }
                                }
                                else
                                {
                                    NPC.frame.X = 0;
                                    NPC.frame.Y = 16 * frameHeight;
                                }
                            }
                            else //First frame of transform animation
                            {
                                NPC.frame.X = 0;
                                NPC.frame.Y = 0;
                            }
                        }
                    }
                    else //Core X
                    {
                        NPC.frame.X = 0;
                        NPC.frameCounter++;
                        if (NPC.frameCounter >= 5)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += 64;
                        }
                        if (NPC.frame.Y >= 8 * 64)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                }
                else if (AI_SubState == (int)Search_Substate.MorphBall)
                {
                    if (AI_Counter % 50 < 10 || AI_Counter % 50 >= 20)
                    {
                        NPC.frame.X = 3 * frameWidth;
                        if (NPC.velocity.Y == 0) //Grounded
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
                else if (AI_SubState == (int)Search_Substate.ScrewAttack)
                {
                    NPC.frame.X = 2 * frameWidth;
                    NPC.frameCounter++;
                    if (NPC.frameCounter >= 3)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                    }
                    if (NPC.frame.Y < 10 * frameHeight || NPC.frame.Y > 17 * frameHeight)
                    {
                        NPC.frame.Y = 10 * frameHeight;
                    }
                }
                else if (AI_SubState == (int)Search_Substate.XRayScope)
                {
                    NPC.frame.Y = 20 * frameHeight;
                    float aim = MathHelper.ToDegrees(Aiming_Rotation);
                    if (AI_Counter > 145 && AI_Counter <= 157)
                    {
                        if (AI_Counter <= 149 || AI_Counter > 153)
                        {
                            NPC.frame.X = 0;
                            NPC.frame.Y = 14 * frameHeight;
                        }
                        else
                        {
                            NPC.frame.X = 0;
                            NPC.frame.Y = 15 * frameHeight;
                        }
                    }
                    else
                    {
                        if (NPC.direction < 0)
                        {
                            aim = 180 - aim;
                        }
                        if (aim > 30)
                        {
                            NPC.frame.X = 0;
                        }
                        else if (aim > -15)
                        {
                            NPC.frame.X = frameWidth;
                        }
                        else if (aim > -60)
                        {
                            NPC.frame.X = frameWidth * 2;
                        }
                        else
                        {
                            NPC.frame.X = frameWidth * 3;
                        }
                    }
                }    
                else
                {
                    NPC.frame.X = 0 * frameWidth;
                    if (NPC.velocity.Y >= 0 && NPC.velocity.Y < 4f) //Grounded
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
                                switch ((int)(AI_Counter % 100) / 10)
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
                                if (AI_Counter % 25 < 8 || AI_Counter % 25 >= 16)
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
                    else //Aerial
                    {
                        if (NPC.frame.Y <= 16 * frameHeight)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y = 16 * frameHeight;
                            NPC.frame.X = 0;
                        }
                        /*NPC.frameCounter++;
                        if (NPC.frameCounter >= 6)
                        {
                            NPC.frame.Y = 17 * frameHeight;
                        }*/
                    }
                }
            }
        }
        public override bool ModifyCollisionData(Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
        {
            return base.ModifyCollisionData(victimHitbox, ref immunityCooldownSlot, ref damageMultiplier, ref npcHitbox);
        }
        public override void AI()
        {
            if (!(State == (int)StateID.Spawn && AI_Counter < 100)) // Hide before spawn explosion
            {
                Lighting.AddLight(new Vector2(NPC.Center.X, NPC.position.Y), 0f, 0.2f, 0f);
            }

            if (State == (int)StateID.Spawn)
            {
                AI_Counter++;
                if (AI_Counter == 30) //Muzzle Flash
                {
                    //Dust.NewDustPerfect(NPC.Center, ModContent.DustType<SAXMuzzleFlash>(), Vector2.Zero);
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileShoot").WithVolumeScale(0.15f), NPC.Center);
                }
                if (AI_Counter >= 30 && AI_Counter < 42)
                {
                    float l = (42 - AI_Counter) / 12f * 0.25f;
                    Lighting.AddLight(NPC.Center, l, l, l);
                }
                if (AI_Counter == 90) //Explosion
                {
                    //Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<DeltaruneExplosion>(), 100, 0); // Placeholder
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<SAXSuperExplosion>(), 10, 10);
                    for (int i = 0; i < 30; i++)
                    {
                        Dust.NewDust(NPC.Center + new Vector2(-90, -90), 180, 180, DustID.Smoke, 0, -2, 0, Color.DarkGray, 2f + Main.rand.NextFloat());
                    }

                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SAXExplosion"), NPC.Center);
                    NPC.direction = Main.rand.NextBool(2) ? 1 : -1;
                }
                if (AI_Counter >= 95 && AI_Counter <= 140 && AI_Counter % 3 == 0) //Secondary Explosions
                {
                    Vector2 exPos = NPC.Center + new Vector2(Main.rand.Next(-25, 26), Main.rand.Next(-40, 31));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), exPos, Vector2.Zero, ModContent.ProjectileType<SAXExplosion>(), 5, 1);

                    for (int i = 0; i < 5; i++)
                    {
                        Dust.NewDust(exPos + new Vector2(-30, -30), 60, 60, DustID.Smoke, 0, -2, 0, default, 1f + Main.rand.NextFloat());
                    }
                }
                if (AI_Counter == 115) //Smoke Clouds
                {
                    Dust.NewDustPerfect(NPC.Center, ModContent.DustType<SAXSmokeCloud>(), new Vector2(-1, 0), 255);
                    Dust.NewDustPerfect(NPC.Center, ModContent.DustType<SAXSmokeCloud>(), Vector2.Zero, 255);
                    Dust.NewDustPerfect(NPC.Center, ModContent.DustType<SAXSmokeCloud>(), new Vector2(1, 0), 255);
                }
                if (AI_Counter >= 115 && AI_Counter <= 150) //Tertiary Explosions and Smoke
                {
                    if (AI_Counter % 2 == 0)
                    {
                        Dust.NewDust(NPC.Center + new Vector2(-30, -40), 60, 50, ModContent.DustType<SAXMiniBoom>());
                    }
                    else
                    {
                        Dust.NewDust(NPC.Center + new Vector2(-30, -AI_Counter + 50), 60, 50, ModContent.DustType<SAXSmokePuff>());
                    }
                }
                if (AI_Counter >= 200)
                {
                    NPC.dontTakeDamage = false;
                    State = (int)StateID.Search;
                    AI_SubState = (int)Search_Substate.TurnAround;
                    AI_Counter = 21; // Puts SA-X right at the end of the turnaround animation
                }
            }
            if (State == (int)StateID.Search)
            {
                //Main.NewText(AI_SubState);
                //Aiming_Rotation = 0;
                if (AI_SubState == (int)Search_Substate.Walking)
                {
                    int frameHeight = 78;
                    if (NPC.frame.Y == frameHeight * 3 || NPC.frame.Y == frameHeight * 8)
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
                    if (AI_Counter == 0)
                    {
                        NPC.velocity.X = WALK_SPEED * NPC.direction;
                    }
                    AI_Counter++;
                    if (NPC.velocity.Y == 0) //Grounded
                    {
                        Point point2 = NPC.Center.ToTileCoordinates();
                        if (WorldGen.InWorld(point2.X, point2.Y, 5) && !NPC.noGravity) // Step up stairs
                        {
                            Vector2 vector16;
                            int width;
                            int height;
                            NPC.GetTileCollisionParameters(out vector16, out width, out height);
                            Vector2 vector17 = NPC.position - vector16;
                            Collision.StepUp(ref vector16, ref NPC.velocity, width, height, ref NPC.stepSpeed, ref NPC.gfxOffY, 1, false, 0);
                            NPC.position = vector16 + vector17;
                        }
                        if (AI_Counter % 600 == 0)
                        {
                            NPC.netUpdate = true;
                            if (Main.expertMode && (Main.rand.NextBool(3) || AI_Counter >= 2400) && AI_Counter >= 1200)
                            {
                                AI_Counter = 0;
                                AI_SubState = (int)Search_Substate.XRayScope;
                            }
                            else
                            {
                                //AI_Counter = 0;
                                AI_SubState = (int)Search_Substate.HeadTurn;
                            }
                        }
                        if (NPC.velocity.X == 0)
                        {
                            //Check for morph tunnel 
                            Vector2 morphCheckPos = new Vector2(NPC.Center.X + (NPC.width / 2 + 14) * NPC.direction, NPC.position.Y + NPC.height - 16) + new Vector2(-14, -16);
                            //for(int i = 0; i < 7; i++)
                            //{
                            //    for (int j = 0; j < 7; j++)
                            //    {
                            //        Dust.NewDustPerfect(morphCheckPos + new Vector2(i * 4, j * 4), DustID.BlueFairy, Vector2.Zero);
                            //    }
                            //}
                            if (Collision.SolidCollision(morphCheckPos + new Vector2(0, -32), 32, 32) && !Collision.SolidCollision(morphCheckPos, 28, 28) && !Main.rand.NextBool(3)) // 2/3 chance
                            {
                                AI_Counter -= AI_Counter % 50;
                                AI_SubState = (int)Search_Substate.MorphBall;
                                NPC.netUpdate = true;
                            }
                            else // Check height of wall and jump accordingly
                            {
                                /*bool aboveLedgeCheck = false;
                                float jumpStrength = 5.5f;
                                Vector2 ledgePos = new Vector2(NPC.direction < 0 ? NPC.position.X - 16 : NPC.position.X, NPC.position.Y - 18);
                                for (int i = 0; i < 17; i++)
                                {
                                    ledgePos.Y -= 16;
                                    jumpStrength += 0.5f;
                                    for (int d = 0; d < 10; d++)
                                    {
                                        Dust.NewDustDirect(ledgePos, NPC.width + 16, NPC.height, DustID.BlueFairy).velocity = Vector2.Zero;
                                    }
                                    if (!Collision.SolidCollision(ledgePos, NPC.width + 16, BASE_HEIGHT))
                                    {
                                        for (int d = 0; d < 20; d++)
                                        {
                                            Dust.NewDustDirect(ledgePos, NPC.width + 16, NPC.height, DustID.GreenFairy).velocity = Vector2.Zero;
                                        }
                                        if (!Collision.SolidCollision(new Vector2(NPC.position.X, ledgePos.Y), NPC.width, (int)(NPC.position.Y - ledgePos.Y)))
                                        {
                                            aboveLedgeCheck = true;
                                        }
                                        break;
                                    }   
                                }*/
                                if (AboveLedgeCheck(out float jumpStrength))
                                {
                                    NPC.velocity.Y = -jumpStrength;
                                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SAXJump").WithVolumeScale(0.5f), NPC.Center);
                                }
                                else
                                {
                                    if (Main.rand.NextBool(10))
                                    {
                                        NPC.direction *= -1;
                                        NPC.velocity.Y = -8;
                                        NPC.velocity.X = 6 * NPC.direction;
                                        AI_SubState = (int)Search_Substate.ScrewAttack;
                                        AI_Counter -= AI_Counter % 250;
                                    }
                                    else
                                    {
                                        if (Main.expertMode && AI_Counter >= 2400)
                                        {
                                            AI_Counter = 0;
                                            AI_SubState = (int)Search_Substate.XRayScope;
                                        }
                                        else
                                        {
                                            AI_Counter -= AI_Counter % 25;
                                            AI_SubState = (int)Search_Substate.TurnAround;
                                        }
                                        Unstuck_Check += 300;
                                    }
                                }
                                NPC.netUpdate = true;
                            }
                        }
                        else if (NPC.velocity.X * NPC.direction < WALK_SPEED)
                        {
                            NPC.velocity.X = WALK_SPEED * NPC.direction;
                        }
                    }
                    else //In Air
                    {
                        if (NPC.velocity.X != 0 && (!Collision.SolidCollision(new Vector2(NPC.position.X, NPC.position.Y + NPC.height), NPC.width, NPC.height * 2) || Main.rand.NextBool(10))
                        && NPC.velocity.Y == NPC.gravity && Main.rand.NextBool(2))
                        {
                            NPC.velocity.Y = -8;
                            NPC.velocity.X = 6 * NPC.direction;
                            AI_SubState = (int)Search_Substate.ScrewAttack;
                            AI_Counter -= AI_Counter % 250;
                            NPC.netUpdate = true;
                        }
                        else
                        {
                            NPC.velocity.X = WALK_SPEED * NPC.direction;
                        }
                    }
                }
                if (AI_SubState == (int)Search_Substate.HeadTurn)
                {
                    NPC.velocity.X = 0;
                    AI_Counter++;
                    if (AI_Counter % 100 == 30)
                    {
                        if (Main.rand.NextBool(3))
                        {
                            //AI_Counter = 0;
                            AI_Counter -= AI_Counter % 25;
                            AI_SubState = (int)Search_Substate.TurnAround;
                            NPC.netUpdate = true;
                        }
                    }
                    if (AI_Counter % 100 >= 90)
                    {
                        //AI_Counter = 0;
                        AI_SubState = (int)Search_Substate.Walking;
                        NPC.velocity.X = WALK_SPEED * NPC.direction;
                    }
                }
                if (AI_SubState == (int)Search_Substate.TurnAround)
                {
                    AI_Counter++;
                    if (AI_Counter % 25 == 16)
                    {
                        NPC.direction *= -1;
                    }
                    if (AI_Counter % 25 >= 24)
                    {
                        //AI_Counter = 0;
                        AI_SubState = (int)Search_Substate.Walking;
                        NPC.velocity.X = WALK_SPEED * NPC.direction;
                        if (AI_Counter % 600 > 400)
                        {
                            AI_Counter += 250;
                        }
                    }
                }
                if (AI_SubState == (int)Search_Substate.MorphBall)
                {
                    //Main.NewText(AI_Counter);
                    if (AI_Counter % 50 < 10) // Crouching
                    {
                        if (NPC.velocity.Y == 0)
                        {
                            if (AI_Counter % 50 == 0)
                            {
                                NPC.position.Y += NPC.height - CROUCH_HEIGHT;
                            }
                            NPC.height = CROUCH_HEIGHT;
                        }
                        else
                        {
                            Aiming_Rotation = MathHelper.PiOver2 * NPC.direction;
                        }
                        AI_Counter++;
                    }
                    else if (AI_Counter % 50 < 20) // Morphball
                    {
                        if (AI_Counter % 50 == 10)
                        {
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/Morph"), NPC.Center);
                            NPC.position.Y += NPC.height - BALL_HEIGHT;
                            NPC.velocity.X = 2.5f * NPC.direction;
                        }

                        if (AI_Counter % 50 < 15)
                            AI_Counter++;
                        NPC.height = BALL_HEIGHT;
                        if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                        {
                            NPC.direction *= -1;
                            NPC.velocity.X = 2 * NPC.direction;
                            Unstuck_Check += 400;
                        }
                        if (NPC.velocity.Y == 0 && AI_Counter % 50 >= 15)
                        {
                            Vector2 unMorphCheckPos = new Vector2(NPC.position.X, NPC.position.Y - (BASE_HEIGHT - NPC.height));
                            if (!Collision.SolidCollision(unMorphCheckPos, BASE_WIDTH, BASE_HEIGHT))
                            {
                                AI_Counter -= AI_Counter % 50;
                                AI_Counter += 20;
                            }
                        }
                        SightCheck(true);

                    }
                    if (AI_Counter % 50 >= 20) //Unmorph
                    {
                        AI_Counter++;
                        if (AI_Counter % 50 <= 30)
                        {
                            if (NPC.velocity.Y == 0)
                            {
                                if (AI_Counter % 50 == 21)
                                {
                                    NPC.position.Y -= CROUCH_HEIGHT - NPC.height;
                                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/UnMorph"), NPC.Center);
                                }
                                NPC.velocity.X = 0;
                                NPC.height = CROUCH_HEIGHT;
                                Aiming_Rotation = 0;
                            }
                            else
                            {
                                if (AI_Counter % 50 == 21)
                                {
                                    NPC.position.Y -= BASE_HEIGHT - NPC.height;
                                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/UnMorph"), NPC.Center);
                                }
                                NPC.height = BASE_HEIGHT;
                                Aiming_Rotation = MathHelper.PiOver2 * NPC.direction;
                            }
                        }
                        else
                        {
                            //AI_Counter = 0;
                            AI_SubState = (int)Search_Substate.Walking;

                            NPC.position.Y -= BASE_HEIGHT - NPC.height;
                            NPC.height = BASE_HEIGHT;
                            NPC.velocity.X = WALK_SPEED * NPC.direction;
                            Aiming_Rotation = 0;
                        }
                    }
                }
                else if (AI_SubState != (int)Search_Substate.XRayScope)
                {
                    SightCheck();
                }
                if (AI_SubState == (int)Search_Substate.ScrewAttack)
                {
                    if (AI_Counter % 250 < 200)
                    {
                        AI_Counter++;
                    }
                    if (NPC.soundDelay <= 0)
                    {
                        NPC.soundDelay = 16;
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SAXScrewAttack"), NPC.Center);
                    }
                    if (NPC.velocity.Y > 3 && AI_Counter % 250 < 200 && !Collision.SolidCollision(new Vector2(NPC.position.X, NPC.position.Y - NPC.height), NPC.width, NPC.height * 2))
                    {
                        NPC.velocity.Y = -8;
                    }
                    if (NPC.velocity.X == 0 && AI_Counter % 250 < 200)
                    {
                        NPC.direction *= -1;
                        Unstuck_Check += 100;
                    }
                    NPC.velocity.X = 6 * NPC.direction;
                    if (NPC.velocity.Y == 0 && NPC.oldVelocity.Y > 0)
                    {
                        AI_SubState = (int)Search_Substate.Walking;
                        NPC.velocity.X = WALK_SPEED * NPC.direction;
                        //AI_Counter = 0;
                    }
                }
                if (AI_SubState == (int)Search_Substate.XRayScope)
                {
                    NPC.velocity.X = 0;
                    AI_Counter++;
                    if (AI_Counter == 1)
                    {
                        Aiming_Rotation = NPC.direction < 0 ? MathHelper.Pi : 0;
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/XRayStart"), NPC.Center);
                    }
                    if (AI_Counter >= 19 && NPC.soundDelay <= 0)
                    {
                        NPC.soundDelay = 13;
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/XRayLoop"), NPC.Center);
                    }
                    if (AI_Counter >= 20)
                    {
                        SightCheck(false, true);
                    }
                    if ((AI_Counter > 20 && AI_Counter <= 60) || (AI_Counter > 150 && AI_Counter <= 230))
                    {
                        Aiming_Rotation += MathHelper.ToRadians(1.875f) * NPC.direction;
                    }
                    if (AI_Counter > 65 && AI_Counter <= 145 || (AI_Counter > 235 && AI_Counter <= 275))
                    {
                        Aiming_Rotation -= MathHelper.ToRadians(1.875f) * NPC.direction;
                    }
                    if (AI_Counter == 151)
                    {
                        NPC.direction *= -1;
                        Aiming_Rotation = MathHelper.Pi - Aiming_Rotation;
                    }

                    //float aim = MathHelper.ToDegrees(Aiming_Rotation);
                    //if (NPC.direction < 0)
                    //{
                    //    aim = 180 - aim;
                    //}
                    //Main.NewText(aim, Color.Yellow);

                    if (AI_Counter > 275)
                    {
                        AI_Counter = 0;
                        AI_SubState = (int)Search_Substate.Walking;
                        NPC.velocity.X = WALK_SPEED * NPC.direction;
                        Aiming_Rotation = 0;
                    }
                }
                if (Unstuck_Check > 0)
                {
                    Unstuck_Check--;
                }
                if (Unstuck_Check > 1000)
                {
                    AI_SubState = (int)Search_Substate.UnstuckTransform;
                    AI_Counter = 0;
                    Unstuck_Check = 0;
                    Aiming_Rotation = 0;
                }

            }
            if (AI_SubState == (int)Search_Substate.UnstuckTransform)
            {
                if (AI_Counter < 150)
                {
                    NPC.velocity.X = 0;
                }
                if (AI_Counter < 200 || AI_Counter >= 300)
                {
                    AI_Counter++;
                }
                if (AI_Counter >= 60) //Begin transform
                {
                    NPC.height = BASE_HEIGHT;
                    if (AI_Counter == 60)
                    {
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/XTransform"), NPC.Center);
                    }
                    if (AI_Counter >= 90)
                    {
                        NPC.noGravity = true;
                        NPC.dontTakeDamage = true;
                        NPC.noTileCollide = true;
                    }
                    if (AI_Counter < 120)
                    {
                        if (AI_Counter % 2 == 0)
                        {
                            TransShaderIntensity = (AI_Counter - 60) / 2 * Main.rand.NextFloat();
                        }
                    }
                    else if (AI_Counter < 150) // Core
                    {
                        if (AI_Counter % 2 == 0)
                        {
                            TransShaderIntensity = (150 - AI_Counter) * Main.rand.NextFloat();
                        }
                    }
                }
                if (AI_Counter == 150)
                {
                    NPC.velocity = (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * 5f;
                }
                if (AI_Counter >= 200 && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) // Terrain Check (WIP)
                {
                    NPC.velocity = Vector2.Zero;
                    if (AI_Counter < 330)
                    {
                        AI_Counter = 330;
                    }
                }
                if (AI_Counter >= 360) //Untransform
                {
                    if (AI_Counter == 360)
                    {
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/XTransform"), NPC.Center);
                    }
                    if (AI_Counter < 420)
                    {
                        if (AI_Counter % 2 == 0)
                        {
                            TransShaderIntensity = (AI_Counter - 360) / 2 * Main.rand.NextFloat();
                        }
                    }
                    else if (AI_Counter < 450)
                    {
                        if (AI_Counter % 2 == 0)
                        {
                            TransShaderIntensity = (450 - AI_Counter) * Main.rand.NextFloat();
                        }
                    }
                    else
                    {
                        NPC.dontTakeDamage = false;
                        NPC.noGravity = false;
                        NPC.noTileCollide = false;
                        AI_Counter = 0;
                        AI_SubState = (int)Search_Substate.Walking;
                        NPC.velocity.Y = 4f;
                        Aiming_Rotation = 0;
                    }
                }
                //if (AI_Counter >= 200)
                //{
                //    AI_Counter = 0;
                //}
            }
            Main.NewText(Unstuck_Check);

            if (State == (int)StateID.Chase)
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
            Vector2 vel = Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200 ? new Vector2(P.velocity.X, 0) : P.velocity;
            Vector2 predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(P.MountedCenter, origin) / speed);
            predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(predictedPos, origin) / speed);
            predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(predictedPos, origin) / speed);
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
            Aiming_Rotation = (float)Math.Atan2(origin.Y - predictedPos.Y, origin.X - predictedPos.X);
        }
        private bool AboveLedgeCheck(out float jumpStrength, int tileHeight = 17)
        {
            bool aboveLedgeCheck = false;
            jumpStrength = 5.5f;
            Vector2 ledgePos = new Vector2(NPC.direction < 0 ? NPC.position.X - 16 : NPC.position.X, NPC.position.Y - 18);
            for (int i = 0; i < tileHeight; i++)
            {
                ledgePos.Y -= 16;
                jumpStrength += 0.5f;
                //for (int d = 0; d < 10; d++)
                //{
                //    Dust.NewDustDirect(ledgePos, NPC.width + 16, NPC.height, DustID.BlueFairy).velocity = Vector2.Zero;
                //}
                if (!Collision.SolidCollision(ledgePos, NPC.width + 16, BASE_HEIGHT))
                {
                    //for (int d = 0; d < 20; d++)
                    //{
                    //    Dust.NewDustDirect(ledgePos, NPC.width + 16, NPC.height, DustID.GreenFairy).velocity = Vector2.Zero;
                    //}
                    if (!Collision.SolidCollision(new Vector2(NPC.position.X, ledgePos.Y), NPC.width, (int)(NPC.position.Y - ledgePos.Y)))
                    {
                        aboveLedgeCheck = true;
                    }
                    break;
                }
            }
            return aboveLedgeCheck;
        }
        private bool SightCheck(bool isMorphed = false, bool xRayScope = false)
        {
            float num = 0f;
            float realDist = 0f;
            bool flag = false;
            int tankTarget = -1;
            bool foundTarget = false;
            if (isMorphed)
            {
                foreach (Player P in Main.ActivePlayers)
                {
                    if (P.active && !P.dead && !P.ghost && (P.GetModPlayer<JoostPlayer>().XInfected || LineOfSight(NPC.Center, P.Center)))
                    {
                        float dist = P.invis ? Math.Max(300 + P.aggro, 0) : 300 + P.aggro / 2;
                        //Main.NewText((int)Vector2.Distance(NPC.Center, P.Center));
                        if (Vector2.Distance(NPC.Center, P.Center) < dist)
                        {
                            TryTrackingTarget(ref num, ref realDist, ref flag, ref tankTarget, P.whoAmI);
                            foundTarget = true;
                        }
                    }
                }
                if (foundTarget)
                {
                    SetTargetTrackingValues(false, realDist, -1);
                    Dust.NewDustPerfect(NPC.targetRect.Center(), DustID.AncientLight, Vector2.Zero, 0, default, 2f).noGravity = true;
                }
                return foundTarget;
            }

            float arcAmount = 150f;
            float angleStart = NPC.direction < 0 ? 180 - 75 : -75;
            if (xRayScope)
            {
                arcAmount = 25f;
                angleStart = MathHelper.ToDegrees(Aiming_Rotation) - 12.5f;
            }    
            Vector2 headPos = NPC.Center + new Vector2(0, -27);
            foreach (Player P in Main.ActivePlayers)
            {
                if (P.active && !P.dead && !P.ghost && (xRayScope || P.GetModPlayer<JoostPlayer>().XInfected || LineOfSight(headPos, P.Center)))
                {
                    float dist = P.invis ? Math.Max(750 + P.aggro, 0) : Math.Max(800 + P.aggro, 200);
                    if (xRayScope)
                    {
                        dist = 300f;
                    }
                    //Main.NewText(dist + " | " + (int)Vector2.Distance(headPos, P.Center));
                    if (dist > 0 && (CheckPointInCircleSector(dist, headPos, P.Center, arcAmount, angleStart) || CheckPointInCircleSector(dist, headPos, new Vector2(P.Center.X, P.position.Y + 8), arcAmount, angleStart) || CheckPointInCircleSector(dist, headPos, new Vector2(P.Center.X, P.position.Y + P.height - 2), arcAmount, angleStart)))
                    {
                        TryTrackingTarget(ref num, ref realDist, ref flag, ref tankTarget, P.whoAmI);
                        foundTarget = true;
                    }
                }
            }
            if (foundTarget)
            {
                SetTargetTrackingValues(false, realDist, -1);
                Dust.NewDustPerfect(NPC.targetRect.Center(), DustID.AmberBolt, Vector2.Zero, 0, default, 2f).noGravity = true;
            }
            return foundTarget;
        }

        //These two methods copied from Terraria.NPC
        private void TryTrackingTarget(ref float distance, ref float realDist, ref bool t, ref int tankTarget, int j)
        {
            float num = Math.Abs(Main.player[j].position.X + (float)(Main.player[j].width / 2) - NPC.position.X + (float)(NPC.width / 2)) + Math.Abs(Main.player[j].position.Y + (float)(Main.player[j].height / 2) - NPC.position.Y + (float)(NPC.height / 2));
            num -= (float)Main.player[j].aggro;
            if (Main.player[j].npcTypeNoAggro[NPC.type] && NPC.direction != 0)
            {
                num += 1000f;
            }
            if (!t || num < distance)
            {
                t = true;
                tankTarget = -1;
                realDist = Math.Abs(Main.player[j].position.X + (float)(Main.player[j].width / 2) - NPC.position.X + (float)(NPC.width / 2)) + Math.Abs(Main.player[j].position.Y + (float)(Main.player[j].height / 2) - NPC.position.Y + (float)(NPC.height / 2));
                distance = num;
                NPC.target = j;
            }
            if (Main.player[j].tankPet >= 0 && !Main.player[j].npcTypeNoAggro[NPC.type])
            {
                int tankPet = Main.player[j].tankPet;
                float num2 = Math.Abs(Main.projectile[tankPet].position.X + (float)(Main.projectile[tankPet].width / 2) - NPC.position.X + (float)(NPC.width / 2)) + Math.Abs(Main.projectile[tankPet].position.Y + (float)(Main.projectile[tankPet].height / 2) - NPC.position.Y + (float)(NPC.height / 2));
                num2 -= 200f;
                if (num2 < distance && num2 < 200f && Collision.CanHit(NPC.Center, 1, 1, Main.projectile[tankPet].Center, 1, 1))
                {
                    tankTarget = tankPet;
                }
            }
        }
        private void SetTargetTrackingValues(bool faceTarget, float realDist, int tankTarget)
        {
            if (tankTarget >= 0)
            {
                NPC.targetRect = new Rectangle((int)Main.projectile[tankTarget].position.X, (int)Main.projectile[tankTarget].position.Y, Main.projectile[tankTarget].width, Main.projectile[tankTarget].height);
                //NPC.direction = 1;
                //if ((float)(NPC.targetRect.X + NPC.targetRect.Width / 2) < NPC.position.X + (float)(NPC.width / 2))
                //{
                //    NPC.direction = -1;
                //}
                //NPC.directionY = 1;
                //if ((float)(NPC.targetRect.Y + NPC.targetRect.Height / 2) < NPC.position.Y + (float)(NPC.height / 2))
                //{
                //    NPC.directionY = -1;
                //}
            }
            else
            {
                if (NPC.target < 0 || NPC.target >= 255)
                {
                    NPC.target = 0;
                }
                NPC.targetRect = new Rectangle((int)Main.player[NPC.target].position.X, (int)Main.player[NPC.target].position.Y, Main.player[NPC.target].width, Main.player[NPC.target].height);
                if (Main.player[NPC.target].dead)
                {
                    faceTarget = false;
                }
                if (Main.player[NPC.target].npcTypeNoAggro[NPC.type] && NPC.direction != 0)
                {
                    faceTarget = false;
                }
                if (faceTarget)
                {
                    int arg_19E_0 = Main.player[NPC.target].aggro;
                    int arg_1D2_0 = (Main.player[NPC.target].height + Main.player[NPC.target].width + NPC.height + NPC.width) / 4;
                    bool flag = NPC.oldTarget >= 0 && NPC.oldTarget <= 254;
                    bool arg_225_0 = Main.player[NPC.target].itemAnimation == 0 && Main.player[NPC.target].aggro < 0;
                    bool flag2 = !NPC.boss;
                    if (!(arg_225_0 & flag & flag2))
                    {
                        NPC.direction = 1;
                        if ((float)(NPC.targetRect.X + NPC.targetRect.Width / 2) < NPC.position.X + (float)(NPC.width / 2))
                        {
                            NPC.direction = -1;
                        }
                        NPC.directionY = 1;
                        if ((float)(NPC.targetRect.Y + NPC.targetRect.Height / 2) < NPC.position.Y + (float)(NPC.height / 2))
                        {
                            NPC.directionY = -1;
                        }
                    }
                }
            }
            if (NPC.confused)
            {
                NPC.direction *= -1;
            }
            if ((NPC.direction != NPC.oldDirection || NPC.directionY != NPC.oldDirectionY || NPC.target != NPC.oldTarget) && !NPC.collideX && !NPC.collideY)
            {
                NPC.netUpdate = true;
            }
        }

        private bool LineOfSight(Vector2 start, Vector2 end)
        {
            //NPC is a copy of Collision.CanHitLine but with a couple checks commented out so the line works in tight spaces
            //Also ignores halfbricks while checking sideways and within 3 tiles
            int x = Math.Max((int)(start.X / 16), 1);
            int y = Math.Max((int)(start.Y / 16), 1);
            int endX = Math.Min((int)(end.X / 16), Main.maxTilesX - 1);
            int endY = Math.Min((int)(end.Y / 16), Main.maxTilesY - 1);

            float xDistInit = Math.Abs(x - endX);
            float yDistInit = Math.Abs(y - endY);
            if (xDistInit == 0f && yDistInit == 0f)
                return true;

            float stepX = 1f;
            float stepY = 1f;
            if (xDistInit == 0f || yDistInit == 0f)
            {
                if (xDistInit == 0f)
                    stepX = 0f;

                if (yDistInit == 0f)
                    stepY = 0f;
            }
            else if (xDistInit > yDistInit)
            {
                stepX = xDistInit / yDistInit;
            }
            else
            {
                stepY = yDistInit / xDistInit;
            }
            //Main.NewText(stepX + ", " + stepY);
            float num9 = 0f;
            float num10 = 0f;
            int checkDir = 1;
            if (y < endY)
                checkDir = 2;

            int xDistCur = (int)xDistInit;
            int yDistCur = (int)yDistInit;
            int xDir = Math.Sign(endX - x);
            int yDir = Math.Sign(endY - y);
            bool flag = false;
            bool flag2 = false;
            int slabCheck = 0;
            try
            {
                do
                {
                    switch (checkDir)
                    {
                        case 2: //Horizontal Checks
                            {
                                num9 += stepX;
                                int num17 = (int)num9;
                                num9 %= 1f;
                                for (int j = 0; j < num17; j++)
                                {
                                    //if (Main.tile[x, y - 1] == null)
                                    //    return false;

                                    if (Main.tile[x, y] == null)
                                        return false;

                                    //if (Main.tile[x, y + 1] == null)
                                    //    return false;

                                    //Tile tile4 = Main.tile[x, y - 1];
                                    //Tile tile5 = Main.tile[x, y + 1];
                                    Tile tile6 = Main.tile[x, y];
                                    Tile tileN = Main.tile[x + xDir, y];
                                    if (//(tile4.HasUnactuatedTile && tile4.HasTile && Main.tileSolid[tile4.TileType] && !Main.tileSolidTop[tile4.TileType]) ||
                                        //(tile5.HasUnactuatedTile && tile5.HasTile && Main.tileSolid[tile5.TileType] && !Main.tileSolidTop[tile5.TileType]) ||
                                        TileCollides(tile6)
                                        && !(tile6.IsHalfBlock && slabCheck < 3))
                                    {
                                        //Dust.NewDustPerfect(new Vector2(x * 16 + 8, y * 16 + 8), DustID.PinkTorch).noGravity = true;
                                        return false;
                                    }
                                    int d = tile6.IsHalfBlock && slabCheck < 3 ? DustID.WhiteTorch : DustID.GreenTorch;
                                    //Dust.NewDustPerfect(new Vector2(x * 16 + 8, y * 16 + 8), d).noGravity = true;
                                    if (xDistCur == 0 && yDistCur == 0)
                                    {
                                        flag = true;
                                        break;
                                    }

                                    x += xDir;
                                    slabCheck++;
                                    xDistCur--;
                                    if (xDistCur == 0 && yDistCur == 0 && num17 == 1)
                                        flag2 = true;
                                }

                                if (yDistCur != 0)
                                    checkDir = 1;

                                break;
                            }
                        case 1: //Vertical Checks
                            {
                                num10 += stepY;
                                int num16 = (int)num10;
                                num10 %= 1f;
                                for (int i = 0; i < num16; i++)
                                {
                                    //if (Main.tile[x - 1, y] == null)
                                    //    return false;

                                    if (Main.tile[x, y] == null)
                                        return false;

                                    //if (Main.tile[x + 1, y] == null)
                                    //    return false;

                                    //Tile tile = Main.tile[x - 1, y];
                                    //Tile tile2 = Main.tile[x + 1, y];
                                    Tile tile3 = Main.tile[x, y];
                                    if (//(tile.HasUnactuatedTile && tile.HasTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType]) ||
                                        //(tile2.HasUnactuatedTile && tile2.HasTile && Main.tileSolid[tile2.TileType] && !Main.tileSolidTop[tile2.TileType]) ||
                                        TileCollides(tile3))
                                    {
                                        //Dust.NewDustPerfect(new Vector2(x * 16 + 8, y * 16 + 8), DustID.PinkTorch).noGravity = true;
                                        return false;
                                    }
                                    //Dust.NewDustPerfect(new Vector2(x * 16 + 8, y * 16 + 8), DustID.GreenTorch).noGravity = true;

                                    if (xDistCur == 0 && yDistCur == 0)
                                    {
                                        flag = true;
                                        break;
                                    }

                                    y += yDir;
                                    yDistCur--;
                                    if (xDistCur == 0 && yDistCur == 0 && num16 == 1)
                                        flag2 = true;
                                }

                                if (xDistCur != 0)
                                    checkDir = 2;

                                break;
                            }
                    }


                    if (Main.tile[x, y] == null)
                        return false;

                    Tile tile7 = Main.tile[x, y];
                    if (TileCollides(tile7) && !(tile7.IsHalfBlock && slabCheck < 3))
                    {
                        //Dust.NewDustPerfect(new Vector2(x * 16 + 8, y * 16 + 8), DustID.RedTorch).noGravity = true;
                        return false;
                    }
                    int dustType = tile7.IsHalfBlock && slabCheck < 3 ? DustID.WhiteTorch : DustID.BlueTorch;
                    //Dust.NewDustPerfect(new Vector2(x * 16 + 8, y * 16 + 8), dustType).noGravity = true;

                } while (!(flag || flag2));

                return true;
            }
            catch
            {
                return false;
            }

        }
        private bool TileCollides(Tile tile)
        {
            return tile.HasUnactuatedTile && tile.HasTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType];
        }
        /* Gave up on making NPC work
        private bool CanSeeThroughSlope(int x, int y, int xDir, int yDir)
        {
            if (Main.tile[x, y] == null)
                return false;

            Tile tile = Main.tile[x, y];
            Tile tileU = Main.tile[x, y - 1];
            Tile tileD = Main.tile[x, y + 1];
            Tile tileL = Main.tile[x - 1, y];
            Tile tileR = Main.tile[x + 1, y];
            if ((xDir > 0 && yDir > 0 && (tile.Slope == SlopeType.SlopeDownLeft && !TileCollides(tileR) || tile.Slope == SlopeType.SlopeUpRight && !TileCollides(tileD))) || //Down Right
                (xDir < 0 && yDir < 0 && (tile.Slope == SlopeType.SlopeDownLeft && !TileCollides(tileU) || tile.Slope == SlopeType.SlopeUpRight && !TileCollides(tileL))) || //Up Left
                (xDir < 0 && yDir > 0 && (tile.Slope == SlopeType.SlopeDownRight && !TileCollides(tileL) || tile.Slope == SlopeType.SlopeUpLeft && !TileCollides(tileD))) || //Down Left
                (xDir > 0 && yDir < 0 && (tile.Slope == SlopeType.SlopeDownRight && !TileCollides(tileU) || tile.Slope == SlopeType.SlopeUpLeft && !TileCollides(tileR))))   //Up Right
                return true;
            return false;
        }
        */
        private bool CheckPointInCircleSector(float radius, Vector2 center, Vector2 target, float arcAngle, float startAngle)
        {
            float endAngle = arcAngle + startAngle;
            float distance = center.Distance(target);
            float rads = center.AngleTo(target);
            float angle = MathHelper.ToDegrees(rads);
            if (endAngle > 360)
            {
                endAngle -= 360;
                startAngle -= 360;
            }
            else if (endAngle > 180 && angle < 0)
            {
                angle += 360;
            }
            //Main.NewText(startAngle, Color.PaleVioletRed);
            //Main.NewText(angle);
            //Main.NewText(endAngle, Color.CornflowerBlue);
            return angle >= startAngle && angle <= endAngle && distance < radius;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (State == (int)StateID.Spawn && AI_Counter < 100) // Hide before spawn explosion
            {
                if (AI_Counter >= 30 && AI_Counter < 52)
                {
                    Texture2D flashTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_MuzzleFlash");
                    int flashFrameCount = 5;
                    int flashFrame = Math.Min(4, (int)((AI_Counter - 30) / 3f));
                    Color flashColor = Color.White;
                    if (AI_Counter > 42)
                    {
                        flashColor *= (52 - AI_Counter) * 0.1f;
                    }    
                    Rectangle flashRect = new Rectangle(0, flashFrame * flashTex.Height / flashFrameCount, flashTex.Width, flashTex.Height / flashFrameCount);
                    Vector2 flashOrigin = new Vector2(flashTex.Width / 2, flashTex.Height / flashFrameCount / 2);
                    Vector2 flashPos = new Vector2(NPC.Center.X - Main.screenPosition.X, NPC.Center.Y - Main.screenPosition.Y);
                    spriteBatch.Draw(flashTex, flashPos, new Rectangle?(flashRect), flashColor, NPC.rotation, flashOrigin, NPC.scale, SpriteEffects.None, 0f);
                }
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
            if (State == (int)StateID.Spawn || State == (int)StateID.Search || State == (int)StateID.Chase)
            {
                Color lightColor = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));

                Texture2D texture = TextureAssets.Npc[NPC.type].Value;

                int xFrameCount = 4;
                int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
                int frameWidth = texture.Width / xFrameCount;
                Rectangle rectangle = new Rectangle(NPC.frame.X, NPC.frame.Y, texture.Width / xFrameCount, texture.Height / Main.npcFrameCount[NPC.type]);
                Vector2 origin = new Vector2(texture.Width / xFrameCount / 2f, texture.Height / Main.npcFrameCount[NPC.type] / 2f);
                Vector2 drawPos = new Vector2(NPC.position.X - Main.screenPosition.X + NPC.width / 2 - texture.Width / xFrameCount / 2f + origin.X, NPC.position.Y - Main.screenPosition.Y + NPC.height - frameHeight + 4f + origin.Y + NPC.gfxOffY);

                DrawData mainData = new DrawData(texture, drawPos, new Rectangle?(rectangle), lightColor, NPC.rotation, origin, NPC.scale, effects, 0f);


                float cannonRotation = Aiming_Rotation;

                if (State == (int)StateID.Search && AI_SubState == (int)Search_Substate.XRayScope)
                {
                    cannonRotation = 0;
                }

                Texture2D cannonTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_ArmCannon");
                Rectangle cannonRect = new Rectangle(0, 0, cannonTex.Width, cannonTex.Height / 2);
                Vector2 cannonOrigin = new Vector2((float)cannonTex.Width / 2, (float)cannonTex.Height / 4);

                int xOff = -2;
                int yOff = -8;
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
                
                DrawData cannonData = new DrawData(cannonTex, NPC.Center - Main.screenPosition + new Vector2(NPC.scale * NPC.direction * xOff, NPC.scale * yOff + NPC.gfxOffY), new Rectangle?(cannonRect), lightColor, cannonRotation, cannonOrigin, NPC.scale, effects, 0f);

                Color scopeColor = AI_Counter % 2 == 0 ? Color.White * 0.75f : Color.White * 0.5f;
                Texture2D scopeTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Scope");
                Vector2 scopePos = new Vector2(NPC.Center.X - Main.screenPosition.X, NPC.Center.Y - 32 - Main.screenPosition.Y + NPC.gfxOffY);
                if (NPC.frame.Y != frameHeight * 15)
                {
                    scopePos.X += 6 * NPC.direction;
                }
                Vector2 scopeOrigin = new Vector2(0, scopeTex.Height / 2);
                Rectangle scopeRect = scopeTex.Bounds;
                Vector2 scopeScale = new Vector2(1f, Math.Min(1f, AI_Counter / 20f)) * 0.5f;
                float scopeRot = Aiming_Rotation;

                DrawData scopeData = new DrawData(scopeTex, scopePos, new Rectangle?(scopeRect), scopeColor, scopeRot, scopeOrigin, scopeScale, SpriteEffects.None, 0f);


                Texture2D visorTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Visor");
                Color visorColor = Color.White;
                if (State == (int)StateID.Search && AI_SubState == (int)Search_Substate.XRayScope)
                {
                    visorTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_VisorWhite");
                    visorColor = AI_Counter % 2 == 0 ? Color.Yellow : new Color(0.6f, 0.6f, 0f);
                }

                DrawData visorData = new DrawData(visorTex, drawPos, new Rectangle?(rectangle), visorColor, NPC.rotation, origin, NPC.scale, effects, 0f);


                Texture2D deathTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Death");
                int deathFrameCount = 10;
                int deathFrameHeight = deathTex.Height / deathFrameCount;
                Rectangle deathRect = new Rectangle(0, NPC.frame.Y, deathTex.Width, deathFrameHeight);
                Vector2 deathOrigin = new Vector2(deathTex.Width / 2f, deathFrameHeight / 2f);
                Vector2 deathDrawPos = new Vector2(NPC.Center.X - Main.screenPosition.X, NPC.position.Y - Main.screenPosition.Y + 3f + NPC.gfxOffY);

                DrawData deathData = new DrawData(deathTex, deathDrawPos, new Rectangle?(deathRect), lightColor, NPC.rotation, deathOrigin, NPC.scale, effects, 0f);


                Texture2D coreTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}CoreX_Core");
                int coreFrameCount = 8;
                int coreFrameHeight = coreTex.Height / coreFrameCount;
                Rectangle coreRect = new Rectangle(0, NPC.frame.Y, coreTex.Width, coreFrameHeight);
                Vector2 coreOrigin = new Vector2(coreTex.Width / 2f, coreFrameHeight / 2f);
                Vector2 coreDrawPos = new Vector2(NPC.Center.X - Main.screenPosition.X, NPC.Center.Y - Main.screenPosition.Y + NPC.gfxOffY);

                DrawData coreData = new DrawData(coreTex, coreDrawPos, new Rectangle?(coreRect), lightColor, NPC.rotation, coreOrigin, NPC.scale, effects, 0f);


                bool useTransformShader = (State == (int)StateID.Search && AI_SubState == (int)Search_Substate.UnstuckTransform)
                    && ((AI_Counter >= 60 && AI_Counter < 150) || (AI_Counter >= 360 && AI_Counter < 450));

                bool isRawCore = (State == (int)StateID.Search && AI_SubState == (int)Search_Substate.UnstuckTransform)
                    && AI_Counter >= 120 && AI_Counter < 420;

                bool useTransformAnim = (State == (int)StateID.Search && AI_SubState == (int)Search_Substate.UnstuckTransform)
                    && AI_Counter >= 40 && AI_Counter < 120;

                if (useTransformShader)
                {
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

                    float intensity = TransShaderIntensity;
                    MiscShaderData shaderData = GameShaders.Misc["JoostXTransform"];
                    shaderData.UseOpacity(intensity);


                    if (isRawCore)
                    {
                        if (AI_Counter >= 390)
                        {
                            shaderData.UseImage0(ModContent.Request<Texture2D>($"{Texture}CoreX_Core"));
                            shaderData.UseImage1(TextureAssets.Npc[NPC.type]);
                            shaderData.Apply(coreData);
                        }
                        else
                        {
                            shaderData.UseImage0(ModContent.Request<Texture2D>($"{Texture}CoreX_Core"));
                            shaderData.UseImage1(ModContent.Request<Texture2D>($"{Texture}CoreX_Core"));
                            shaderData.Apply(coreData);
                        }
                    }
                    else
                    {
                        if (AI_Counter >= 420)
                        {
                            shaderData.UseImage0(TextureAssets.Npc[NPC.type]);
                            shaderData.UseImage1(TextureAssets.Npc[NPC.type]);
                            shaderData.Apply(mainData);
                            mainData.Draw(spriteBatch);
                        }
                        else if (AI_Counter < 90)
                        {
                            shaderData.UseImage0(ModContent.Request<Texture2D>($"{Texture}_Death"));
                            shaderData.UseImage1(ModContent.Request<Texture2D>($"{Texture}_Death"));
                            shaderData.Apply(deathData);
                        }
                        else
                        {
                            shaderData.UseImage0(ModContent.Request<Texture2D>($"{Texture}_Death"));
                            shaderData.UseImage1(ModContent.Request<Texture2D>($"{Texture}CoreX_Core"));
                            shaderData.Apply(deathData);
                        }
                    }
                }
                
                if (useTransformAnim)
                {
                    deathData.Draw(spriteBatch);
                }
                else if (isRawCore)
                {
                    coreData.Draw(spriteBatch);
                }
                else if (!useTransformShader)
                {
                    if (!(NPC.frame.X == 0 && NPC.frame.Y >= 14 * frameHeight && NPC.frame.Y <= 17 * frameHeight) &&            // Turnaround and falling
                    !(NPC.frame.X == 4 * frameWidth && NPC.frame.Y == 17) &&                                                    // Shinespark
                    !(NPC.frame.X == 1 * frameWidth && NPC.frame.Y >= 10 * frameHeight && NPC.frame.Y <= 17 * frameHeight) &&   // Morphball
                    !(NPC.frame.X == 2 * frameWidth && NPC.frame.Y >= 10 * frameHeight && NPC.frame.Y <= 17 * frameHeight))     // Screw Attack
                    {
                        cannonData.Draw(spriteBatch);
                    }

                    mainData.Draw(spriteBatch);

                    if (State == (int)StateID.Search && AI_SubState == (int)Search_Substate.XRayScope)
                    {
                        scopeData.Draw(spriteBatch);
                    }

                    visorData.Draw(spriteBatch);
                }

                if (useTransformShader)
                {
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
                }
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
                return NPC.CanHitPlayer(target, ref cooldownSlot);
            }
            return false;
        }
        public override bool CanHitNPC(NPC target)
        {
            if ((Math.Abs(target.Center.Y - NPC.Center.Y) < (target.height / 2) + 25 && Math.Abs(target.Center.X - NPC.Center.X) < (target.width / 2) + 25) && NPC.localAI[1] <= 0)
            {
                return NPC.CanHitNPC(target);
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

