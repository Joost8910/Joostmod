using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class CrimsonCactuar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactuar");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 54;
            NPC.damage = 40;
            NPC.defense = 25;
            NPC.lifeMax = 400;
            if (NPC.downedMoonlord)
            {
                NPC.damage = 80;
                NPC.defense = 50;
                NPC.lifeMax = 1600;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 6, 0);
            NPC.knockBackResist = 0.75f;
            NPC.aiStyle = -1;
            NPC.noGravity = false;
            Banner = Mod.Find<ModNPC>("Cactuar").Type;
            BannerItem = Mod.Find<ModItem>("CactuarBanner").Type;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.ai[3] < 200)
            {
                if (NPC.velocity.Y == 0)
                {
                    NPC.frameCounter += Math.Abs(NPC.velocity.X);
                    if (NPC.frameCounter > 30)
                    {
                        if (NPC.frame.Y == 0)
                        {
                            NPC.frame.Y = 62;
                        }
                        else
                        {
                            NPC.frame.Y = 0;
                        }
                        NPC.frameCounter = 0;
                    }
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 4)
                {
                    if (NPC.frame.Y != 62)
                    {
                        NPC.frame.Y = 62;
                    }
                    else
                    {
                        NPC.frame.Y = 124;
                    }
                    NPC.frameCounter = 0;
                }
            }
        }
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Cactus, 10);
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Anniversary").Type, 1);
            }
            if (Main.expertMode)
            {
                if (Main.rand.Next(1, 8) <= 1)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.DarkShard, 1);
                }
            }
            else
            if (Main.rand.Next(1, 10) <= 1)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.DarkShard, 1);
            }

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactuar1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactuar2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactuar2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactuar2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactuar2"), 1f);
            }
            NPC.ai[3]++;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.PlayerInTown && ((!spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.SpawnTileY > Main.worldSurface) && !spawnInfo.Player.ZoneBeach && spawnInfo.Player.ZoneDesert && spawnInfo.Player.ZoneCrimson && Main.hardMode ? 0.15f : 0f;
        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
                NPC.ai[3] = 0;
            }
            NPC.netUpdate = true;
            if (NPC.velocity.Y == 0)
            {
                NPC.rotation = 0;
            }
            if (((Collision.CanHitLine(NPC.Center, 1, 1, P.position, P.width, P.height) && NPC.Distance(P.Center) < 550) || NPC.ai[3] > 0) && NPC.velocity.Y == 0)
            {
                NPC.ai[3]++;
            }
            if (NPC.ai[3] <= 0)
            {
                if (NPC.direction == 0)
                {
                    NPC.direction = 1;
                }
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0)
                {
                    NPC.velocity.Y = -8;
                    if (Main.rand.Next(4) == 0)
                    {
                        NPC.direction *= -1;
                        NPC.netUpdate = true;
                    }
                }
                if (NPC.velocity.X * NPC.direction < 3)
                {
                    NPC.velocity.X += NPC.direction * 0.11f;
                }
                if (NPC.velocity.X > 3 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = 3;
                }
                if (NPC.velocity.X < -3 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = -3;
                }
                if (NPC.velocity.Y != 0)
                {
                    NPC.rotation = NPC.ai[1] * 0.0174f * NPC.direction;
                    NPC.ai[1] += 20;
                }
                else
                {
                    NPC.ai[1] = 0;
                }
            }
            else if (NPC.ai[3] < 200)
            {
                NPC.direction = (P.Center.X < NPC.Center.X ? -1 : 1);
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0)
                {
                    NPC.velocity.Y = -8;
                }
                if (NPC.velocity.X * NPC.direction < 5.5f)
                {
                    NPC.velocity.X += NPC.direction * 0.11f;
                }
                if (NPC.velocity.X > 5f && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = 5f;
                }
                if (NPC.velocity.X < -5f && NPC.velocity.Y == 0)
                {
                    NPC.velocity.X = -5f;
                }
                if (NPC.velocity.Y != 0)
                {
                    NPC.rotation = NPC.ai[1] * 0.0174f * NPC.direction;
                    NPC.ai[1] += 20;
                }
                else
                {
                    NPC.ai[1] = 0;
                }
                NPC.ai[2]++;
                if (NPC.Distance(P.Center) < 200 && NPC.velocity.Y == 0 && NPC.ai[2] > 80)
                {
                    NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (NPC.position.Y + NPC.height)));
                    NPC.velocity.X = 7 * NPC.direction;
                    NPC.ai[2] = 0;
                }
            }
            if (NPC.ai[3] >= 200 && NPC.velocity.Y == 0)
            {
                NPC.direction = (P.Center.X < NPC.Center.X ? -1 : 1);
                NPC.velocity.X = 0;
                if (NPC.ai[3] >= 210)
                {
                    float speed = 16f;
                    int type = Mod.Find<ModProjectile>("CactusNeedle2").Type;
                    if (NPC.ai[3] % 5 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item7, NPC.Center);
                    }
                    Vector2 dir = NPC.DirectionTo(P.Center);
                    Vector2 vel = new Vector2(dir.X, dir.Y).RotatedByRandom(MathHelper.ToRadians(5));
                    dir = vel * speed;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, dir.X, dir.Y, type, 1, 0f, Main.myPlayer);
                    }
                }
                if ((NPC.ai[3] >= 225 && !Main.expertMode) || (Main.expertMode && NPC.ai[3] >= 240))
                {
                    NPC.ai[3] = 0;
                    NPC.velocity.X += NPC.direction;
                }
            }

        }
    }
}

