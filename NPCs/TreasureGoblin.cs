using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
            NPC.HitSound = SoundID.NPCHit40.WithPitchVariance(.75f);
            NPC.DeathSound = SoundID.NPCDeath45.WithPitchVariance(.75f);
            NPC.value = 0f;
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
            NPC.ai[1] += 5;
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/TreasureGoblin"), 1f);
                for (int i = 0; i < 4; i++)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.position.X, NPC.position.Y, Main.rand.Next(-10, 10), Main.rand.Next(-10, -5), 518, 0, 0f, Main.myPlayer);
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        //Projectile.NewProjectile(npc.position.X, npc.position.Y, Main.rand.Next(-7, 7), Main.rand.Next(-7, -1), 413, 0, 0f, Main.myPlayer);
                        Item.NewItem(NPC.Center, NPC.width, NPC.height, ItemID.GoldCoin);
                        SoundEngine.PlaySound(SoundID.Coins, NPC.Center);
                    }
                }
            }
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
                        Item.NewItem((int)NPC.Center.X - 16 * NPC.direction, (int)NPC.Center.Y, 2, 2, ItemID.GoldCoin);
                        SoundEngine.PlaySound(SoundID.Coins, NPC.Center);
                        NPC.localAI[0]++;
                    }
                }
            }
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
                    Dust.NewDustDirect(new Vector2(NPC.Center.X + 20 * NPC.direction, NPC.Center.Y + 6), 1, 1, 16);
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
                    Item.NewItem((int)NPC.Center.X + 36 * NPC.direction, (int)NPC.Center.Y + 6, 12, 12, ItemID.GoldCoin);
                    NPC.active = false;
                    if (Main.netMode == 2)
                    {
                        NPC.netSkip = -1;
                        NPC.life = 0;
                        NetMessage.SendData(23, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
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

