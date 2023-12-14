using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Hostile;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class StormWyvernHead : StormWyvern
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Wyvern");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.WyvernHead);
            NPC.aiStyle = -1;
            NPC.lifeMax = 15000;
            NPC.damage = 80;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath8;
            NPC.value = 0;
            NPC.netAlways = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.SpawnTileY < Main.worldSurface * 0.35f && Main.raining && !JoostWorld.downedStormWyvern && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override void OnKill()
        {
            JoostWorld.downedStormWyvern = true;
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(NPC, ModContent.ItemType<Items.Quest.StormWyvern>(), Main.rand, 1, 1, 1, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EvilStone>(), 100));
        }
        public override void Init()
        {
            base.Init();
            head = true;
            directional = true;
            wyvernStyle = true;
        }
        public override void CustomSegmentSpawn()
        {
            if (NPC.ai[0] == 0)
            {
                NPC.ai[3] = (float)NPC.whoAmI;
                NPC.realLife = NPC.whoAmI;
                int lastSeg = NPC.whoAmI;
                for (int i = 0; i < maxLength; i++)
                {
                    int segType = ModContent.NPCType<StormWyvernBody>();
                    if (i == 1)
                    {
                        segType = ModContent.NPCType<StormWyvernWings>();
                    }
                    if (i == 8)
                    {
                        segType = ModContent.NPCType<StormWyvernLegs>();
                    }
                    if (i == maxLength - 3)
                    {
                        segType = ModContent.NPCType<StormWyvernBody2>();
                    }
                    if (i == maxLength - 2)
                    {
                        segType = ModContent.NPCType<StormWyvernBody3>();
                    }
                    if (i == maxLength - 1)
                    {
                        segType = ModContent.NPCType<StormWyvernTail>();
                    }
                    int num10 = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), segType, NPC.whoAmI);
                    Main.npc[num10].ai[3] = (float)NPC.whoAmI;
                    Main.npc[num10].realLife = NPC.whoAmI;
                    Main.npc[num10].ai[1] = (float)lastSeg;
                    Main.npc[lastSeg].ai[0] = (float)num10;
                    NPC.netUpdate = true;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num10, 0f, 0f, 0f, 0, 0, 0);
                    lastSeg = num10;
                }
            }

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale) + 1;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)NPC.localAI[2]);
            writer.Write((short)NPC.localAI[3]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[2] = reader.ReadInt16();
            NPC.localAI[3] = reader.ReadInt16();
        }

        //Only localAI[2] and localAI[3] available for use
        public override bool PreAI()
        {
            var source = NPC.GetSource_FromAI();
            Main.raining = true;
            Main.maxRaining = 0.9f;
            if (Main.numClouds < Main.maxClouds)
            {
                Main.numClouds++;
            }
            if (Math.Abs(Main.windSpeedCurrent) < Math.Abs(Main.windSpeedTarget))
            {
                Main.windSpeedCurrent += Math.Sign(Main.windSpeedTarget) * 0.015f;
            }
            Main.rainTime = 60;
            Player target = Main.player[NPC.target];
            if (Main.expertMode && Math.Abs(Main.windSpeedCurrent) > 0.8f)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.position.Y / 16 < Main.worldSurface && !p.behindBackWall)
                    {
                        p.AddBuff(BuffID.WindPushed, 2, false);
                    }
                }
            }
            if (Vector2.Distance(NPC.Center, target.Center) > 3500 || (target.position.Y / 16f) > Main.worldSurface || NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
                target = Main.player[NPC.target];
                if (!target.active || target.dead || Vector2.Distance(NPC.Center, target.Center) > 3000 || (target.position.Y / 16f) > Main.worldSurface)
                {
                    NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)((float)NPC.lifeMax * 0.001f) : NPC.lifeMax;
                    if (NPC.localAI[3] > 0)
                    {
                        NPC.localAI[3]--;
                    }
                    if (NPC.Center.Y < 666 && NPC.velocity.Y < speed)
                    {
                        NPC.velocity.Y += turnSpeed * 2;
                    }
                    if (NPC.Center.Y / 16 >= Main.worldSurface && NPC.velocity.Y > speed)
                    {
                        NPC.velocity.Y -= turnSpeed * 2;
                    }
                    return true;
                }
            }

            //Lightning
            if (NPC.localAI[3] > 0)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 55);
                if (NPC.localAI[3] == 12 || NPC.localAI[3] == 24)
                {
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_lightning_aura_zap_1"), NPC.Center); //21
                }
                if (NPC.localAI[3] == 168)
                {
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sky_dragons_fury_circle_1"), NPC.Center); //224
                }
                NPC.localAI[3]++;
                if (NPC.localAI[3] < 240)
                {
                    float s = speed;
                    Vector2 vel = NPC.velocity.RotatedBy(MathHelper.ToRadians(3 * NPC.spriteDirection));
                    vel *= s / vel.Length();
                    NPC.velocity = vel;
                    NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
                }
                else if (NPC.localAI[3] < 288)
                {
                    float s = 4;
                    NPC.velocity = NPC.DirectionTo(target.Center) * s;
                    NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
                }
                if (NPC.localAI[3] == 288)
                {
                    NPC.velocity *= -1;
                    SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_sky_dragons_fury_shot_0").WithPitchOffset(-0.5f), NPC.Center); //226
                }
                if (NPC.localAI[3] >= 288 && NPC.localAI[3] < 318)
                {
                    float s = 3;
                    NPC.velocity = NPC.DirectionTo(target.Center) * -s;
                    NPC.rotation = (float)Math.Atan2(-NPC.velocity.Y, -NPC.velocity.X) + 1.57f;
                }
                if (NPC.localAI[3] == 318)
                {
                    NPC.velocity *= -1;
                    SoundEngine.PlaySound(SoundID.Item122.WithVolumeScale(1.5f), NPC.Center);
                    
                    Vector2 vel = NPC.velocity;
                    vel.Normalize();
                    Projectile.NewProjectile(source,NPC.Center, vel, ModContent.ProjectileType<StormWyvernZap>(), 65, 0, 0, NPC.whoAmI);
                }
                if (NPC.localAI[3] >= 338)
                {
                    NPC.localAI[3] = 0;
                }
                return false;
            }
            else
            {
                //Periodic Dash
                NPC.localAI[2]++;
                if (NPC.localAI[2] % 400 == 300)
                {
                    SoundEngine.PlaySound(SoundID.NPCHit56, NPC.Center);
                }
                if (NPC.localAI[2] % 400 > 300 && NPC.localAI[2] % 400 < 340 || Vector2.Distance(NPC.Center, target.Center) > 1800)
                {
                    Vector2 vel = NPC.DirectionTo(target.Center) * speed;
                    if (vel.Length() > speed)
                    {
                        vel *= speed / vel.Length();
                    }
                    float home = 20f;
                    if (vel != Vector2.Zero)
                    {
                        NPC.velocity = ((home - 1f) * NPC.velocity + vel) / home;
                    }
                }
                if (NPC.localAI[2] % 400 == 340)
                {
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_betsy_fireball_shot_0"), NPC.Center); //28
                    float dashSpeed = speed * 2;
                    Vector2 targetPos = target.Center;
                    if (Main.expertMode)
                    {
                        targetPos += target.velocity * (Vector2.Distance(NPC.Center, target.Center) / dashSpeed);
                    }
                    NPC.velocity = NPC.DirectionTo(targetPos) * dashSpeed;
                }
                if (NPC.localAI[2] % 400 > 340)
                {
                    Vector2 targetPos = target.Center;
                    if (Main.expertMode)
                    {
                        targetPos += target.velocity * (Vector2.Distance(NPC.Center, target.Center) / NPC.velocity.Length());
                    }
                    Vector2 direction = new Vector2(targetPos.X > NPC.Center.X ? 1 : -1, targetPos.Y > NPC.Center.Y ? 1 : -1);
                    if (Math.Abs(NPC.velocity.X) < speed)
                    {
                        NPC.velocity.X += turnSpeed * direction.X;
                    }
                    if (Math.Abs(NPC.velocity.Y) < speed)
                    {
                        NPC.velocity.Y += turnSpeed * direction.Y;
                    }
                }
                if (NPC.localAI[2] > 1500)
                {
                    NPC.localAI[2] = 0;
                    NPC.localAI[3] = 1;
                }
            }
            return true;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = 0;
            if (NPC.localAI[3] > 0)
            {
                if (NPC.localAI[3] < 8)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (NPC.localAI[3] < 16)
                {
                    NPC.frame.Y = frameHeight * 2;
                }
                else
                {
                    NPC.frame.Y = frameHeight * 3;
                }
            }
            else if (NPC.localAI[2] % 400 >= 300 && NPC.localAI[2] % 400 < 340)
            {
                NPC.frame.Y = frameHeight;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            int xFrameCount = 1;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rect = new Rectangle(NPC.frame.X, NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vect = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[NPC.type]) / 2f));
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2f) - (texture.Width / xFrameCount) / 2f + vect.X, NPC.position.Y - Main.screenPosition.Y + (float)(NPC.height / 2) - (float)(texture.Height / Main.npcFrameCount[NPC.type]) / 2 + 4f + vect.Y), new Rectangle?(rect), drawColor, NPC.rotation, vect, 1f, effects, 0f);

            if (NPC.localAI[3] > 0 && NPC.localAI[3] < 318)
            {
                float scale = NPC.localAI[3] < 288 ? NPC.localAI[3] / 288 : 1f;
                float rot = MathHelper.ToRadians(NPC.localAI[3] * 11);
                texture = TextureAssets.Projectile[ModContent.ProjectileType<StormWyvernZap>()].Value;
                rect = new Rectangle((texture.Width / 3) * ((int)NPC.localAI[3] / 2) % 3, 0, (texture.Width / 3), (texture.Height / 3));
                vect = new Vector2(((texture.Width / 3) / 2f), ((texture.Height / 3) / 2f));
                Vector2 offSet = (NPC.rotation - 1.57f).ToRotationVector2() * 60;
                spriteBatch.Draw(texture, offSet + new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2f) - (texture.Width / 3) / 2f + vect.X, NPC.position.Y - Main.screenPosition.Y + (float)(NPC.height / 2) - (float)(texture.Height / 3) / 2 + 4f + vect.Y), new Rectangle?(rect), Color.White, rot, vect, scale, effects, 0f);

            }
            return false;
        }
    }
}