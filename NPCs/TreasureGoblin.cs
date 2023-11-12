using JoostMod.Items.Accessories;
using JoostMod.Items.Ammo;
using JoostMod.Items.Placeable;
using JoostMod.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
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
            Main.npcFrameCount[NPC.type] = 40;
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 40;
            NPC.rarity = 4;
            NPC.damage = 0;
            NPC.defense = 20;
            if (Main.expertMode)
            {
                NPC.lifeMax = 3000;
            }
            else
            {
                NPC.lifeMax = 1500;
            }
            NPC.HitSound = SoundID.NPCHit40 with 
            { 
                PitchVariance = 0.75f 
            };
            NPC.DeathSound = SoundID.NPCDeath45 with
            {
                PitchVariance = 0.75f
            };
            NPC.value = 10000;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            Banner = NPC.type;
            BannerItem = Mod.Find<ModItem>("TreasureGoblinBanner").Type;
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (NPC.ai[3] > 2)
            {
                crit = true;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (NPC.ai[3] > 2)
            {
                crit = true;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 2f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.PlayerInTown && !Main.pumpkinMoon && !Main.snowMoon && !spawnInfo.Sky && !Main.eclipse && spawnInfo.SpawnTileY < Main.rockLayer && Main.hardMode && !NPC.AnyNPCs(NPC.type) ? 0.0017f : 0f;
        }
        /*
        public override void OnKill()
        {
            Player player = Main.player[NPC.target];
            if (Main.rand.NextBool(10))
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FourthAnniversary").Type, 1);
            }
            bool drop = false;
            while (!drop)
            {
                if (Main.rand.Next(10) == 0 && player.ZoneHallow)
                {
                    Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.HallowedKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneJungle)
                {
                    Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.JungleKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneCorrupt)
                {
                    Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.CorruptionKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneCrimson)
                {
                    Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.CrimsonKey);
                    drop = true;
                }
                if (Main.rand.Next(10) == 0 && player.ZoneSnow)
                {
                    Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.FrozenKey);
                    drop = true;
                }
                if (Main.expertMode)
                {
                    if (Main.rand.NextBool(100))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("EvilStone").Type, 1);
                    }
                    else if (Main.rand.NextBool(99))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SkullStone").Type, 1);
                    }
                    else if (Main.rand.NextBool(98))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("JungleStone").Type, 1);
                    }
                    else if (Main.rand.NextBool(97))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("InfernoStone").Type, 1);
                    }
                    else if (Main.rand.NextBool(96))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SeaStoneDeep").Type, 1);
                    }
                    else if (Main.rand.NextBool(95))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SeaStoneEast").Type, 1);
                    }
                    else if (Main.rand.NextBool(94))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SeaStoneHigh").Type, 1);
                    }
                    else if (Main.rand.NextBool(93))
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SeaStoneWest").Type, 1);
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.PDA);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("StoneofJordan").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FrozenOrb").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("HungeringArrow").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("TwinChakrams").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Whirlwind").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(5) < 2)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("PlagueOfToads").Type, 1);
                        drop = true;
                    }

                }
                else
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.PDA);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("StoneofJordan").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FrozenOrb").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("HungeringArrow").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("TwinChakrams").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Whirlwind").Type, 1);
                        drop = true;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("PlagueOfToads").Type, 1);
                        drop = true;
                    }
                }
            }
        }
        */

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 7,
                MaximumItemDropsCount = 13,
            };
            npcLoot.Add(new DropOneByOne(ItemID.GoldCoin, parameters));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FourthAnniversary>(), 20));
            npcLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(
                ItemDropRule.NormalvsExpert(ItemID.PDA, 4, 3),
                ItemDropRule.NormalvsExpert(ModContent.ItemType<StoneofJordan>(), 4, 3),
                ItemDropRule.NormalvsExpert(ModContent.ItemType<FrozenOrb>(), 4, 3),
                ItemDropRule.NormalvsExpert(ModContent.ItemType<HungeringArrow>(), 4, 3),
                ItemDropRule.NormalvsExpert(ModContent.ItemType<TwinChakrams>(), 4, 3),
                ItemDropRule.NormalvsExpert(ModContent.ItemType<Whirlwind>(), 4, 3),
                ItemDropRule.NormalvsExpert(ModContent.ItemType<PlagueOfToads>(), 4, 3)));

            LeadingConditionRule stones = new LeadingConditionRule(new Conditions.IsExpert());
            stones.OnSuccess(ItemDropRule.OneFromOptions(100,
                ModContent.ItemType<EvilStone>(),
                ModContent.ItemType<SkullStone>(),
                ModContent.ItemType<JungleStone>(),
                ModContent.ItemType<InfernoStone>(),
                ModContent.ItemType<SeaStoneHigh>(),
                ModContent.ItemType<SeaStoneEast>(),
                ModContent.ItemType<SeaStoneWest>(),
                ModContent.ItemType<SeaStoneDeep>()));
            npcLoot.Add(stones);

            //Need to test this especiialy to make sure it doesnt use vanilla key drop rates
            LeadingConditionRule jKey = new LeadingConditionRule(new Conditions.JungleKeyCondition());
            LeadingConditionRule coKey = new LeadingConditionRule(new Conditions.CorruptKeyCondition());
            LeadingConditionRule crKey = new LeadingConditionRule(new Conditions.CrimsonKeyCondition());
            LeadingConditionRule hKey = new LeadingConditionRule(new Conditions.HallowKeyCondition());
            LeadingConditionRule fKey = new LeadingConditionRule(new Conditions.FrozenKeyCondition());
            LeadingConditionRule dKey = new LeadingConditionRule(new Conditions.DesertKeyCondition());
            jKey.OnSuccess(ItemDropRule.Common(ItemID.JungleKey, 10));
            coKey.OnSuccess(ItemDropRule.Common(ItemID.CorruptionKey, 10));
            crKey.OnSuccess(ItemDropRule.Common(ItemID.CrimsonKey, 10));
            hKey.OnSuccess(ItemDropRule.Common(ItemID.HallowedKey, 10));
            fKey.OnSuccess(ItemDropRule.Common(ItemID.FrozenKey, 10));
            dKey.OnSuccess(ItemDropRule.Common(ItemID.DungeonDesertKey, 10));
            npcLoot.Add(jKey);
            npcLoot.Add(coKey);
            npcLoot.Add(crKey);
            npcLoot.Add(hKey);
            npcLoot.Add(fKey);
            npcLoot.Add(dKey);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.ai[3] > 2)
            {
                if (NPC.frame.Y < 10 * frameHeight)
                {
                    NPC.frame.Y = 10 * frameHeight;
                }
                if (NPC.ai[3] % 8 == 0 && NPC.frame.Y < frameHeight * 39)
                {
                    NPC.frame.Y = (NPC.frame.Y + frameHeight);
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.ai[1] <= 0)
                {
                    if (NPC.frameCounter < 90)
                    {
                        if (NPC.frameCounter % 15 == 0)
                        {
                            NPC.frame.Y = (NPC.frame.Y + frameHeight);
                        }
                        if (NPC.frame.Y >= frameHeight * 2)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                    else if (NPC.frameCounter < 150)
                    {
                        if (NPC.frameCounter % 12 == 0 && NPC.frame.Y < frameHeight * 3)
                        {
                            NPC.frame.Y = (NPC.frame.Y + frameHeight);
                        }
                    }
                    else if (NPC.frameCounter < 180)
                    {
                        NPC.frame.Y = frameHeight * 4;
                    }
                    else
                    {
                        if (NPC.frameCounter % 12 == 0)
                        {
                            NPC.frame.Y = (NPC.frame.Y + frameHeight);
                        }
                        if (NPC.frame.Y >= frameHeight * 8)
                        {
                            NPC.frame.Y = 0;
                            NPC.frameCounter = 0;
                        }
                    }
                }
                else
                {
                    if (NPC.frame.Y < frameHeight * 8)
                    {
                        NPC.frame.Y = frameHeight * 8;
                    }
                    if (NPC.frameCounter >= 3)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = (NPC.frame.Y + frameHeight);
                    }
                    if (NPC.frame.Y >= frameHeight * 10)
                    {
                        NPC.frame.Y = frameHeight * 8;
                    }
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TreasureGoblin").Type);
            }

            //The HitEffect hook is client side, these bits will need to be moved
            NPC.ai[1] += 5;
        }
        public override bool CheckDead()
        {
            SoundEngine.PlaySound(SoundID.Coins, NPC.Center);
            for (int i = 0; i < 4; i++)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_Death(), NPC.position.X, NPC.position.Y, Main.rand.Next(-10, 10), Main.rand.Next(-10, -5), ProjectileID.CoinPortal, 0, 0f, Main.myPlayer);
                }
            }
            return base.CheckDead();
        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || P.dead || !P.active)
            {
                NPC.TargetClosest(false);
            }
            if (NPC.velocity.X > 0)
            {
                NPC.direction = 1;
            }
            if (NPC.velocity.X < 0)
            {
                NPC.direction = -1;
            }
            Lighting.AddLight((int)(NPC.Center.X + 24 * NPC.direction) / 16, (int)(NPC.Center.Y + 12) / 16, 0.08f, 0.66f, 0.7f);
            if (NPC.ai[2] < 496)
            {
                NPC.ai[2] += 1 + Main.rand.Next(5);
                NPC.netUpdate = true;
            }
            else
            {
                NPC.ai[2]++;
            }
            if (NPC.ai[2] == 500)
            {
                SoundEngine.PlaySound(SoundID.Zombie79.WithPitchOffset(0.3f), NPC.position);
                SoundEngine.PlaySound(SoundID.NPCHit40.WithPitchOffset(0.4f), NPC.position);
            }
            if (NPC.ai[2] == 510)
            {
                SoundEngine.PlaySound(SoundID.NPCHit40.WithPitchOffset(0.4f), NPC.position);
            }
            if (NPC.ai[2] == 515)
            {
                SoundEngine.PlaySound(SoundID.NPCHit40.WithPitchOffset(0.6f), NPC.position);
                NPC.ai[2] = 0;
            }
            if (NPC.ai[1] > 1 && NPC.ai[3] <= 0)
            {
                if (NPC.velocity.X == 0)
                {
                    if (NPC.velocity.Y >= 0)
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[1] > 120)
                        {
                            NPC.ai[1] = 2;
                            NPC.ai[3] = 1;
                            //npc.position.X += npc.direction * 8;
                        }
                    }
                    if (NPC.velocity.Y < 0)
                    {
                        NPC.rotation = -90 * NPC.direction * 0.0174f;
                    }
                    NPC.velocity.Y = -5f;
                }
                else
                {
                    NPC.ai[1] = 2;
                    NPC.rotation = 0;
                }
                if (NPC.Center.X < P.Center.X)
                {
                    NPC.velocity.X = -6f;
                }
                else
                {
                    NPC.velocity.X = 6f;
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

                if (NPC.ai[0] % 60 == 0 && NPC.ai[1] > 1)
                {
                    if (NPC.localAI[0] < 30 && !NPC.noTileCollide)
                    {
                        //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -3, 413, 0, 0f, Main.myPlayer);
                        Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.Center.X - 16 * NPC.direction, (int)NPC.Center.Y, 2, 2, ItemID.GoldCoin);
                        SoundEngine.PlaySound(SoundID.Coins, NPC.Center);
                        NPC.localAI[0]++;
                    }
                }
            }
            //Escape Portal
            if (NPC.ai[3] > 0)
            {
                NPC.defense = 0;
                NPC.rotation = 0;
                NPC.velocity.X = 0;
                if (NPC.velocity.Y == 0 || NPC.ai[3] > 2)
                {
                    NPC.ai[3]++;
                    NPC.noGravity = true;
                }
                if (NPC.ai[3] == 8)
                {
                    SoundEngine.PlaySound(SoundID.Dig, NPC.Center);
                    Dust.NewDustDirect(new Vector2(NPC.Center.X + 20 * NPC.direction, NPC.Center.Y + 6), 1, 1, DustID.Cloud);
                }
                if (NPC.ai[3] == 24)
                {
                    SoundEngine.PlaySound(SoundID.DoubleJump, NPC.Center);
                }
                if (NPC.ai[3] == 32 || NPC.ai[3] == 48 || NPC.ai[3] == 64)
                {
                    SoundEngine.PlaySound(SoundID.Grass, NPC.Center);
                }
                if (NPC.ai[3] == 16 || NPC.ai[3] == 40 || NPC.ai[3] == 72)
                {
                    SoundEngine.PlaySound(SoundID.Coins, NPC.Center);
                }
                if (NPC.ai[3] == 56)
                {
                    SoundEngine.PlaySound(SoundID.Shatter, NPC.Center);
                }
                if (NPC.ai[3] == 80)
                {
                    SoundEngine.PlaySound(SoundID.Grab, NPC.Center);
                }
                if (NPC.ai[3] == 88 || NPC.ai[3] == 112)
                {
                    SoundEngine.PlaySound(SoundID.Item7, NPC.Center);
                }
                if (NPC.ai[3] == 116)
                {
                    SoundEngine.PlaySound(SoundID.Coins, NPC.Center);
                    SoundEngine.PlaySound(SoundID.Dig, NPC.Center);
                }
                if (NPC.ai[3] == 156)
                {
                    SoundEngine.PlaySound(SoundID.Grab, NPC.Center);
                }
                if (NPC.ai[3] == 176)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                }
                if (NPC.ai[3] >= 224)
                {
                    NPC.dontTakeDamage = true;
                }
                if (NPC.ai[3] >= 240)
                {
                    Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.Center.X + 36 * NPC.direction, (int)NPC.Center.Y + 6, 12, 12, ItemID.GoldCoin);
                    NPC.active = false;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NPC.netSkip = -1;
                        NPC.life = 0;
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            if (NPC.direction == 1)
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

