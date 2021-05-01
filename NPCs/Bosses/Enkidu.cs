using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class Enkidu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enkidu");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 120;
            npc.damage = 150;
            npc.defense = 20;
            npc.lifeMax = 280000;
            npc.HitSound = SoundID.NPCHit6;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 0;
            npc.noTileCollide = true;
            npc.boss = true;
            bossBag = mod.ItemType("GilgBag");
            npc.buffImmune[BuffID.Ichor] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ClashOnTheBigBridge");
            npc.noGravity = true;
            npc.frameCounter = 0;
            musicPriority = MusicPriority.BossHigh;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override bool PreNPCLoot()
        {
            for (int i = 0; i < 15; i++)
            {
                Item.NewItem(npc.getRect(), ItemID.Heart);
            }
            return !NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2"));
        }
        public override void NPCLoot()
        {
            if (!JoostWorld.downedGilgamesh)
                Main.NewText("With Gilgamesh and Enkidu's defeat, you can now fish the legendary stones from their respective biomes", 125, 25, 225);
            JoostWorld.downedGilgamesh = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GenjiToken"), 1 + Main.rand.Next(2));
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("COTBBMusicBox"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GilgameshMask"));
                }
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GilgameshTrophy"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FifthAnniversary"), 1);
            }
        }

        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    npc.velocity = new Vector2(0f, -100f);
                    npc.active = false;
                }
            }
            npc.netUpdate = true;
            npc.ai[1]++;
            if ((npc.ai[1] % 15) == 0)
            {
                npc.ai[2]++;
            }
            if (npc.ai[1] < 1200 || (!NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2"))))
            {
                if ((npc.ai[2] % 4) == 0)
                {
                    if (npc.ai[1] % 60 == 0)
                    {
                        npc.velocity = npc.DirectionTo(P.Center) * (npc.Distance(P.Center) / 15);
                    }
                }
                else
                {
                    npc.velocity = npc.DirectionTo(P.Center) * (npc.Distance(P.Center) / 30);
                }
            }
            if (((!NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2"))) || npc.ai[1] >= 900)) // Massive Wind Attack
            {
                //TODO reverse projectile direction for future rework
                float Speed = npc.Center.X > P.Center.X ? -10 : 10;
                if (NPC.AnyNPCs(mod.NPCType("Gilgamesh")) || NPC.AnyNPCs(mod.NPCType("Gilgamesh2")))
                {
                    npc.velocity = npc.DirectionTo(P.Center + new Vector2(0, -300)) * (npc.Distance(P.Center + new Vector2(0, -300)) / 50);
                    npc.ai[2] = 0;
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

                if (npc.ai[1] % 4 == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 34);
                    if (Main.netMode != 1)
                    {
                        //TODO Center on player for future enkidu rework
                        Projectile.NewProjectile(npc.Center.X + (Main.rand.Next(-15, 15) * 120), npc.Center.Y - (120 * 8), Speed, Math.Abs(Speed), mod.ProjectileType("EnkiduWind"), 50, 15f, Main.myPlayer);
                    }
                }
            }
            if (npc.ai[1] >= 2000)
            {
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
            }
            npc.rotation = npc.velocity.X * 0.0174f * 2.5f;
            npc.ai[3]++;
            if ((npc.ai[1] < 875 || (!NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2")))))
            {
                if (npc.ai[3] >= 90) // Triple Wind Attack
                {
                    float Speed = 10f + npc.velocity.Length();
                    int damage = 50;
                    int type = mod.ProjectileType("EnkiduWind");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 32);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    float spread = 45f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((float)((Math.Cos(rotation) * Speed) * -1) * (float)((Math.Cos(rotation) * Speed) * -1) + (float)((Math.Sin(rotation) * Speed) * -1) * (float)((Math.Sin(rotation) * Speed) * -1));
                    double startAngle = Math.Atan2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)) - spread / 3;
                    double deltaAngle = spread / 3;
                    double offsetAngle;
                    if (Main.netMode != 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, 15f, Main.myPlayer);
                        }
                    }
                    npc.ai[3] = 0;
                }
                if (npc.ai[3] > 75)
                {
                    npc.velocity = npc.DirectionTo(P.Center) * (npc.Distance(P.Center) / 50);
                    npc.rotation = npc.direction * 0.0174f * -10f;
                }
                if (npc.ai[3] < 15)
                {
                    npc.velocity = npc.DirectionTo(P.Center) * -4;
                    npc.rotation = npc.velocity.X * 0.0174f * 2.5f;
                }
            }
        }
        public override bool CheckDead()
        {
            if (NPC.AnyNPCs(mod.NPCType("Gilgamesh")) || NPC.AnyNPCs(mod.NPCType("Gilgamesh2")))
            {
                Main.NewText("<Enkidu> I'm out of here.", 25, 225, 25);
                Main.NewText("<Gilgamesh> Hey! Sidekicks are NOT to abandon the hero!", 225, 25, 25);
            }
            for (int i = 0; i < 80; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Smoke, 0, 0, 0, Color.Green, 2);
            }
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if ((npc.ai[1] < 875 || (!NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2")))))
            {
                if (npc.ai[3] > 75)
                {
                    npc.frame.Y = 0;
                    npc.frameCounter = 5;
                }
                if (npc.ai[3] < 15)
                {
                    npc.frameCounter++;
                }
            }
            npc.frameCounter++;
            if (npc.frameCounter > 6)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 160);
            }
            if (npc.frame.Y >= 800)
            {
                npc.frame.Y = 0;
            }
        }
    }
}

