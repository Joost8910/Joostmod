using System;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.ItemDropRules.DropConditions;
using Terraria.DataStructures;
using JoostMod.Items.Consumables;
using JoostMod.Items.Materials;
using JoostMod.Items.Armor;
using JoostMod.Projectiles.Hostile;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class Enkidu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enkidu");
            Main.npcFrameCount[NPC.type] = 5;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused,
                    BuffID.Ichor
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 120;
            NPC.damage = 150;
            NPC.defense = 20;
            NPC.lifeMax = 280000;
            NPC.HitSound = SoundID.NPCHit6;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = 0f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.noTileCollide = true;
            NPC.boss = true;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("GilgBag").Type;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ClashOnTheBigBridge");
            NPC.noGravity = true;
            NPC.frameCounter = 0;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override bool PreKill()
        {
            /*
            for (int i = 0; i < 15; i++)
            {
                Item.NewItem(NPC.GetSource_FromAI(), NPC.getRect(), ItemID.Heart);
            }*/
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type);
        }
        public override void OnKill()
        {
            if (!JoostWorld.downedGilgamesh && Main.netMode != NetmodeID.Server)
                Main.NewText("With Gilgamesh and Enkidu's defeat, you can now fish the legendary stones from their respective biomes", 125, 25, 225);
            JoostWorld.downedGilgamesh = true;
            /*
            if (Main.expertMode)
            {
                NPC.DropBossBags();
            }
            else
            {
                Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GenjiToken").Type, 1 + Main.rand.Next(2));
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem(NPC.GetSource_FromAI(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("COTBBMusicBox").Type);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GilgameshMask").Type);
                }
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GilgameshTrophy").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FifthAnniversary").Type, 1);
            }
            */
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule rule = new LeadingConditionRule(new GilgameshDropCondition());
            rule.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<GilgBag>()));
            rule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GilgameshTrophy>(), 10));
            rule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FifthAnniversary>(), 10));
            rule.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<GenjiToken>(), 1, 1, 2));
            rule.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<COTBBMusicBox>(), 4));
            rule.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<GilgameshMask>(), 7));
            npcLoot.Add(rule);
            npcLoot.Add(new DropOneByOne(ItemID.Heart, new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 10,
                MaximumItemDropsCount = 15,
            }));
        }
        public override void AI()
        {
            NPC.ai[0]++;
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
            NPC.netUpdate = true;
            NPC.ai[1]++;
            if ((NPC.ai[1] % 15) == 0)
            {
                NPC.ai[2]++;
            }
            if (NPC.ai[1] < 1200 || (!NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type)))
            {
                if ((NPC.ai[2] % 4) == 0)
                {
                    if (NPC.ai[1] % 60 == 0)
                    {
                        NPC.velocity = NPC.DirectionTo(P.Center) * (NPC.Distance(P.Center) / 15);
                    }
                }
                else
                {
                    NPC.velocity = NPC.DirectionTo(P.Center) * (NPC.Distance(P.Center) / 30);
                }
            }
            if (((!NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type)) || NPC.ai[1] >= 900)) // Massive Wind Attack
            {
                //TODO reverse projectile direction for future rework
                float Speed = NPC.Center.X > P.Center.X ? -10 : 10;
                if (NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type))
                {
                    NPC.velocity = NPC.DirectionTo(P.Center + new Vector2(0, -300)) * (NPC.Distance(P.Center + new Vector2(0, -300)) / 50);
                    NPC.ai[2] = 0;
                    Speed *= 0.75f;
                }

                /*
                Main.raining = true;
                Main.maxRaining = 0.9f;
                if (Main.numClouds < Main.maxClouds)
                {
                    Main.numClouds++;
                }
                Main.windSpeedSet = (Speed < 0 ? -1 : 1);
                if (Math.Abs(Main.windSpeed) < Math.Abs(Main.windSpeedSet))
                {
                    Main.windSpeed += Math.Sign(Main.windSpeedSet) * 0.03f;
                }
                Main.rainTime = 10;
                if (Main.expertMode && Math.Abs(Main.windSpeed) > 0.5f)
                {
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player p = Main.player[i];
                        if (p.active && p.position.Y / 16 < Main.worldSurface && !p.behindBackWall)
                        {
                            p.AddBuff(194, 2, false);
                        }
                    }
                }
                */

                if (NPC.ai[1] % 4 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item34, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        //TODO Center on player for future enkidu rework
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + (Main.rand.Next(-15, 15) * 120), NPC.Center.Y - (120 * 8), Speed, Math.Abs(Speed), ModContent.ProjectileType<EnkiduWind>(), 50, 15f, Main.myPlayer);
                    }
                }
            }
            if (NPC.ai[1] >= 2000)
            {
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
            }
            NPC.rotation = NPC.velocity.X * 0.0174f * 2.5f;
            NPC.ai[3]++;
            if ((NPC.ai[1] < 875 || (!NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type))))
            {
                if (NPC.ai[3] >= 90) // Triple Wind Attack
                {
                    float Speed = 10f + NPC.velocity.Length();
                    int damage = 50;
                    int type = ModContent.ProjectileType<EnkiduWind>();
                    SoundEngine.PlaySound(SoundID.Item32, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    float spread = 45f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((float)((Math.Cos(rotation) * Speed) * -1) * (float)((Math.Cos(rotation) * Speed) * -1) + (float)((Math.Sin(rotation) * Speed) * -1) * (float)((Math.Sin(rotation) * Speed) * -1));
                    double startAngle = Math.Atan2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)) - spread / 3;
                    double deltaAngle = spread / 3;
                    double offsetAngle;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, 15f, Main.myPlayer);
                        }
                    }
                    NPC.ai[3] = 0;
                }
                if (NPC.ai[3] > 75)
                {
                    NPC.velocity = NPC.DirectionTo(P.Center) * (NPC.Distance(P.Center) / 50);
                    NPC.rotation = NPC.direction * 0.0174f * -10f;
                }
                if (NPC.ai[3] < 15)
                {
                    NPC.velocity = NPC.DirectionTo(P.Center) * -4;
                    NPC.rotation = NPC.velocity.X * 0.0174f * 2.5f;
                }
            }
        }
        public override bool CheckDead()
        {
            if (Main.netMode != NetmodeID.Server && (NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type)))
            {
                Main.NewText("<Enkidu> I'm out of here.", 25, 225, 25);
                Main.NewText("<Gilgamesh> Hey! Sidekicks are NOT to abandon the hero!", 225, 25, 25);
            }
            for (int i = 0; i < 80; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0, 0, 0, Color.Green, 2);
            }
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if ((NPC.ai[1] < 875 || (!NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type))))
            {
                if (NPC.ai[3] > 75)
                {
                    NPC.frame.Y = 0;
                    NPC.frameCounter = 5;
                }
                if (NPC.ai[3] < 15)
                {
                    NPC.frameCounter++;
                }
            }
            NPC.frameCounter++;
            if (NPC.frameCounter > 6)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 160);
            }
            if (NPC.frame.Y >= 800)
            {
                NPC.frame.Y = 0;
            }
        }
    }
}

