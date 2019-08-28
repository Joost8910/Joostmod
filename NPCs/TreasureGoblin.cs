using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    [AutoloadBossHead]
    public class TreasureGoblin : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Goblin");
            Main.npcFrameCount[npc.type] = 10;
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 44;
            npc.rarity = 4;
            npc.damage = 0;
            npc.defense = 40;
            if (Main.expertMode)
            {
                npc.lifeMax = 3000;
            }
            else
            {
                npc.lifeMax = 1500;
            }
            npc.HitSound = SoundID.NPCHit40.WithPitchVariance(.75f);
            npc.DeathSound = SoundID.NPCDeath45.WithPitchVariance(.75f);
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            banner = npc.type;
            bannerItem = mod.ItemType("TreasureGoblinBanner");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 2f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.playerInTown && !Main.pumpkinMoon && !Main.snowMoon && !spawnInfo.sky && !Main.eclipse && spawnInfo.spawnTileY < Main.rockLayer && Main.hardMode && !NPC.AnyNPCs(npc.type) ? 0.0017f : 0f;
        }
        public override void NPCLoot()
        {
            Player player = Main.player[npc.target];
            bool drop = false;
            while (!drop)
            {
                if (Main.rand.Next(10) == 0 && player.ZoneHoly)
                {
                    Item.NewItem(npc.Center, npc.width, npc.height, ItemID.HallowedKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneJungle)
                {
                    Item.NewItem(npc.Center, npc.width, npc.height, ItemID.JungleKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneCorrupt)
                {
                    Item.NewItem(npc.Center, npc.width, npc.height, ItemID.CorruptionKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneCrimson)
                {
                    Item.NewItem(npc.Center, npc.width, npc.height, ItemID.CrimsonKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneSnow)
                {
                    Item.NewItem(npc.Center, npc.width, npc.height, ItemID.FrozenKey);
                    drop = true;
                }
                if (Main.expertMode)
                {
                    if (Main.rand.Next(100) == 0)
                    {
                        switch (Main.rand.Next(8))
                        {
                            case 1:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkullStone"), 1);
                                break;
                            case 2:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JungleStone"), 1);
                                break;
                            case 3:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernoStone"), 1);
                                break;
                            case 4:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneDeep"), 1);
                                break;
                            case 5:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneEast"), 1);
                                break;
                            case 6:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneHigh"), 1);
                                break;
                            case 7:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneWest"), 1);
                                break;
                            default:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
                                break;
                        }
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem(npc.Center, npc.width, npc.height, ItemID.PDA);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StoneofJordan"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FrozenOrb"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HungeringArrow"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TwinChakrams"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Whirlwind"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PlagueOfToads"), 1);
                        drop = true;
                    }

                }
                else
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem(npc.Center, npc.width, npc.height, ItemID.PDA);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StoneofJordan"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FrozenOrb"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HungeringArrow"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TwinChakrams"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Whirlwind"), 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PlagueOfToads"), 1);
                        drop = true;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.ai[1] <= 0)
            {
                if (npc.frameCounter >= 20)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 48);
                }
                if (npc.frame.Y >= 336)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            if (npc.frameCounter >= 3)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 48);
            }
            if (npc.frame.Y >= 480)
            {
                npc.frame.Y = 384;
            }

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[1] += 5;
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TreasureGoblin"), 1f);
                for (int i = 0; i < 4; i++)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, Main.rand.Next(-10, 10), Main.rand.Next(-10, -5), 518, 0, 0f, Main.myPlayer);
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        //Projectile.NewProjectile(npc.position.X, npc.position.Y, Main.rand.Next(-7, 7), Main.rand.Next(-7, -1), 413, 0, 0f, Main.myPlayer);
                        Item.NewItem(npc.Center, npc.width, npc.height, ItemID.GoldCoin);
                        Main.PlaySound(18, npc.Center);
                    }
                }
            }
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || P.dead || !P.active)
            {
                npc.TargetClosest(false);
            }
            if (npc.velocity.X > 0)
            {
                npc.direction = 1;
            }
            if (npc.velocity.X < 0)
            {
                npc.direction = -1;
            }
            Lighting.AddLight((int)(npc.Center.X + 24 * npc.direction) / 16, (int)(npc.Center.Y + 12) / 16, 0.08f, 0.66f, 0.7f);
            if (npc.ai[2] < 496)
            {
                npc.ai[2] += 1 + Main.rand.Next(5);
                npc.netUpdate = true;
            }
            else
            {
                npc.ai[2]++;
            }
            if (npc.ai[2] == 500)
            {
                Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 79, 1, 0.3f);
                Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 40, 1, 0.4f);
            }
            if (npc.ai[2] == 510)
            {
                Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 40, 1, 0.4f);
            }
            if (npc.ai[2] == 515)
            {
                Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 40, 1, 0.6f);
                npc.ai[2] = 0;
            }
            if (npc.ai[1] > 1)
            {
                if (npc.velocity.X == 0)
                {
                    npc.velocity.Y = -5f;
                    npc.ai[1]++;
                    if (npc.ai[1] > 60)
                    {
                        npc.ai[1] = 2;
                        npc.position.X += npc.direction * 8;
                    }
                }
                else
                {
                    npc.ai[1] = 2;
                }
                if (npc.Center.X < P.Center.X)
                {
                    npc.velocity.X = -6f;
                }
                else
                {
                    npc.velocity.X = 6f;
                }
                if (Collision.SolidCollision(npc.position + new Vector2(8, 8), 8, 28))
                {
                    npc.velocity.X *= 0.3f;
                    npc.velocity.Y = -2f;
                    npc.noTileCollide = true;
                    npc.color = new Color(255, 89, 200, 200);
                    Dust.NewDust(npc.position, npc.width, npc.height, 255);
                }
                else
                {
                    npc.noTileCollide = false;
                    npc.color = Color.White;
                }
            }

            if (npc.ai[0] % 60 == 0 && npc.ai[1] > 1)
            {
                if (npc.ai[3] < 30 && !npc.noTileCollide)
                {
                    //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -3, 413, 0, 0f, Main.myPlayer);
                    Item.NewItem(npc.Center, npc.width, npc.height, ItemID.GoldCoin);
                    Main.PlaySound(18, npc.Center);
                    npc.ai[3]++;
                }
            }
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            if (npc.direction == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
        }

    }
}

