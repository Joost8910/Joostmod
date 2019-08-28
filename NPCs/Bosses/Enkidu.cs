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
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 160;
            npc.damage = 150;
            npc.defense = 38;
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
            JoostWorld.downedGilgamesh = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GenjiToken"), 1 + Main.rand.Next(2));
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("COTBBMusicBox"));
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GilgameshMask"));
                }
                if (Main.rand.Next(5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GilgameshTrophy"));
                }
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
                        npc.velocity = npc.DirectionTo(P.Center) * 18;
                    }
                }
                else
                {
                    npc.velocity = npc.DirectionTo(P.Center) * 7;
                }
            }
            if (((!NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2"))) || npc.ai[1] >= 900))
            {
                float Speed = npc.Center.X > P.Center.X ? -10 : 10;
                if (NPC.AnyNPCs(mod.NPCType("Gilgamesh")) || NPC.AnyNPCs(mod.NPCType("Gilgamesh2")))
                {
                    npc.velocity = npc.DirectionTo(P.Center) * 2;
                    npc.ai[2] = 0;
                    Speed *= 0.75f;
                }
                if (npc.ai[1] % 4 == 0)
                {
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 34);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X + (Main.rand.Next(-15, 15) * npc.width), npc.Center.Y - (npc.width * 8), Speed, Math.Abs(Speed), mod.ProjectileType("EnkiduWind"), 50, 15f, Main.myPlayer);
                    }
                }
            }
            if (npc.ai[1] >= 2000)
            {
                npc.ai[1] = 0;
                npc.ai[2] = 0;
            }
            npc.ai[3]++;
            if (npc.ai[3] >= 90 && (npc.ai[1] < 900 || (!NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2")))))
            {
                float Speed = 15f;
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
            if(npc.Distance(P.Center) > 800)
            {
                npc.velocity = npc.DirectionTo(P.Center) * (npc.Distance(P.Center) / 50);
            }

        }
        public override bool CheckDead()
        {
            if (NPC.AnyNPCs(mod.NPCType("Gilgamesh")) || NPC.AnyNPCs(mod.NPCType("Gilgamesh2")))
            {
                Main.NewText("<Enkidu> I'm out of here.", 25, 225, 25);
                Main.NewText("<Gilgamesh> Hey! Sidekicks are NOT to abandon the hero!", 225, 25, 25);
            }
            for (int i = 0; i < npc.width / 8; i++)
            {
                for (int j = 0; j < npc.height / 8; j++)
                {
                    Dust.NewDust(npc.position + new Vector2(i, j), 8, 8, DustID.Smoke, 0, 0, 0, Color.Green, 2);
                }
            }
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 160);
            }
            if (npc.frame.Y >= 640)
            {
                npc.frame.Y = 0;
            }
        }
    }
}

