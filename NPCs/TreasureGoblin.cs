using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
            Main.npcFrameCount[npc.type] = 30;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;
            npc.rarity = 4;
            npc.damage = 0;
            npc.defense = 20;
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
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.ai[3] > 2)
            {
                crit = true;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (npc.ai[3] > 2)
            {
                crit = true;
            }
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
                    if (Main.rand.NextBool(100))
                    {
                        switch (Main.rand.Next(4))
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
                            default:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
                                break;
                        }
                    }
                    else if (Main.rand.NextBool(100))
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneDeep"), 1);
                                break;
                            case 2:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneEast"), 1);
                                break;
                            case 3:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneHigh"), 1);
                                break;
                            default:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SeaStoneWest"), 1);
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
            if (npc.ai[3] > 2)
            {
                if (npc.frame.Y < 10 * frameHeight)
                {
                    npc.frame.Y = 10 * frameHeight;
                }
                if (npc.ai[3] % 10 == 0 && npc.frame.Y < frameHeight * 29)
                {
                    npc.frame.Y = (npc.frame.Y + frameHeight);
                }
            }
            else
            {
                npc.frameCounter++;
                if (npc.ai[1] <= 0)
                {
                    if (npc.frameCounter < 90)
                    {
                        if (npc.frameCounter % 15 == 0)
                        {
                            npc.frame.Y = (npc.frame.Y + frameHeight);
                        }
                        if (npc.frame.Y >= frameHeight * 2)
                        {
                            npc.frame.Y = 0;
                        }
                    }
                    else if (npc.frameCounter < 150)
                    {
                        if (npc.frameCounter % 12 == 0 && npc.frame.Y < frameHeight * 3)
                        {
                            npc.frame.Y = (npc.frame.Y + frameHeight);
                        }
                    }
                    else if (npc.frameCounter < 180)
                    {
                        npc.frame.Y = frameHeight * 4;
                    }
                    else
                    {
                        if (npc.frameCounter % 12 == 0)
                        {
                            npc.frame.Y = (npc.frame.Y + frameHeight);
                        }
                        if (npc.frame.Y >= frameHeight * 8)
                        {
                            npc.frame.Y = 0;
                            npc.frameCounter = 0;
                        }
                    }
                }
                else
                {
                    if (npc.frame.Y < frameHeight * 8)
                    {
                        npc.frame.Y = frameHeight * 8;
                    }
                    if (npc.frameCounter >= 3)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (npc.frame.Y + frameHeight);
                    }
                    if (npc.frame.Y >= frameHeight * 10)
                    {
                        npc.frame.Y = frameHeight * 8;
                    }
                }
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
            if (npc.ai[1] > 1 && npc.ai[3] <= 0)
            {
                if (npc.velocity.X == 0)
                {
                    if (npc.velocity.Y >= 0)
                    {
                        npc.ai[1]++;
                        if (npc.ai[1] > 120)
                        {
                            npc.ai[1] = 2;
                            npc.ai[3] = 1;
                            //npc.position.X += npc.direction * 8;
                        }
                    }
                    if (npc.velocity.Y < 0)
                    {
                        npc.rotation = -90 * npc.direction * 0.0174f;
                    }
                    npc.velocity.Y = -5f;
                }
                else
                {
                    npc.ai[1] = 2;
                    npc.rotation = 0;
                }
                if (npc.Center.X < P.Center.X)
                {
                    npc.velocity.X = -6f;
                }
                else
                {
                    npc.velocity.X = 6f;
                }/*
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
                */

                if (npc.ai[0] % 60 == 0 && npc.ai[1] > 1)
                {
                    if (npc.localAI[0] < 30 && !npc.noTileCollide)
                    {
                        //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -3, 413, 0, 0f, Main.myPlayer);
                        Item.NewItem((int)npc.Center.X - 16 * npc.direction, (int)npc.Center.Y, 2, 2, ItemID.GoldCoin);
                        Main.PlaySound(18, npc.Center);
                        npc.localAI[0]++;
                    }
                }
            }
            if (npc.ai[3] > 0)
            {
                npc.defense = 0;
                npc.rotation = 0;
                npc.velocity.X = 0;
                if (npc.velocity.Y == 0 || npc.ai[3] > 2)
                {
                    npc.ai[3]++;
                    npc.noGravity = true;
                }
                if (npc.ai[3] == 90)
                {
                    Main.PlaySound(18, npc.Center);
                    Main.PlaySound(0, npc.Center);
                }
                if (npc.ai[3] == 120)
                {
                    Main.PlaySound(2, npc.Center, 8);
                }
                if (npc.ai[3] >= 180)
                {
                    npc.dontTakeDamage = true;
                }
                if (npc.ai[3] >= 200)
                {
                    Item.NewItem((int)npc.Center.X + 36 * npc.direction, (int)npc.Center.Y + 6, 12, 12, ItemID.GoldCoin);
                    npc.active = false;
                    if (Main.netMode == 2)
                    {
                        npc.netSkip = -1;
                        npc.life = 0;
                        NetMessage.SendData(23, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
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

