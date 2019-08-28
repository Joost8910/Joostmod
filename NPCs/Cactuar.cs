using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class Cactuar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactuar");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 54;
            npc.damage = 40;
            npc.defense = 18;
            npc.lifeMax = 300;
            if (NPC.downedMoonlord)
            {
                npc.damage = 80;
                npc.defense = 36;
                npc.lifeMax = 1200;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.knockBackResist = 0.75f;
            npc.aiStyle = -1;
            npc.noGravity = false;
            banner = mod.NPCType("Cactuar");
            bannerItem = mod.ItemType("CactuarBanner");
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.ai[3] < 200)
            {
                if (npc.velocity.Y == 0)
                {
                    npc.frameCounter += Math.Abs(npc.velocity.X);
                    if (npc.frameCounter > 30)
                    {
                        if (npc.frame.Y == 0)
                        {
                            npc.frame.Y = 62;
                        }
                        else
                        {
                            npc.frame.Y = 0;
                        }
                        npc.frameCounter = 0;
                    }
                }
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter > 4)
                {
                    if (npc.frame.Y != 62)
                    {
                        npc.frame.Y = 62;
                    }
                    else
                    {
                        npc.frame.Y = 124;
                    }
                    npc.frameCounter = 0;
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Cactus Person") && !NPC.AnyNPCs(mod.NPCType("JumboCactuar")))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Cactus, 10);
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Anniversary"), 1);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactuar1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactuar2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactuar2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactuar2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactuar2"), 1f);
            }
            npc.ai[3]++;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.player.ZoneBeach && !spawnInfo.playerInTown && ((!spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.spawnTileY > Main.worldSurface) && spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneHoly && Main.hardMode ? 0.15f : 0f;
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
                npc.ai[3] = 0;
                npc.netUpdate = true;
            }
            if (npc.velocity.Y == 0)
            {
                npc.rotation = 0;
            }
            bool cactusPersonNear = false;
            int cactusPerson = -1;
            if (!NPC.AnyNPCs(mod.NPCType("JumboCactuar")))
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC cactu = Main.npc[k];
                    if (cactu.active && cactu.type == mod.NPCType("Cactus Person") && npc.Distance(cactu.Center) < 800)
                    {
                        cactusPersonNear = true;
                        cactusPerson = cactu.whoAmI;
                        break;
                    }
                }
            }
            if (((Collision.CanHitLine(npc.Center, 1, 1, P.position, P.width, P.height) && npc.Distance(P.Center) < 550) || npc.ai[3] > 0) && npc.velocity.Y == 0 && (!cactusPersonNear || npc.ai[3] > 0))
            {
                npc.ai[3]++;
            }
            if (npc.ai[3] <= 0)
            {
                if (cactusPersonNear)
                {
                    npc.damage = 0;
                }
                else
                {
                    npc.damage = 40;
                    if (NPC.downedMoonlord)
                    {
                        npc.damage = 80;
                    }
                    if (Main.expertMode)
                    {
                        npc.damage *= 2;
                    }
                }
                if (npc.direction == 0)
                {
                    npc.direction = 1;
                }
                if (npc.velocity.Y == 0 && npc.velocity.X == 0 && npc.localAI[1] < 500)
                {
                    npc.velocity.Y = -8;
                    if (Main.rand.Next(4) == 0)
                    {
                        npc.direction *= -1;
                        npc.netUpdate = true;
                    }
                }
                if (npc.velocity.X * npc.direction < 3)
                {
                    npc.velocity.X += npc.direction * 0.11f;
                }
                if (npc.velocity.X > 3 && npc.velocity.Y == 0)
                {
                    npc.velocity.X = 3;
                }
                if (npc.velocity.X < -3 && npc.velocity.Y == 0)
                {
                    npc.velocity.X = -3;
                }
                npc.localAI[1]++;
                if (npc.localAI[1] > 500)
                {
                    npc.velocity.X = 0;
                }
                if (npc.localAI[1] > 600)
                {
                    npc.localAI[1] = 0;
                    npc.direction *= -1;
                    npc.velocity.X += npc.direction;
                }
                if (npc.velocity.Y != 0)
                {
                    npc.rotation = npc.ai[1] * 0.0174f * npc.direction;
                    npc.ai[1] += 20;
                }
                else
                {
                    npc.ai[1] = 0;
                }
            }
            else if (npc.ai[3] < 200)
            {
                if (cactusPersonNear && npc.life >= npc.lifeMax)
                {
                    npc.ai[3] = 0;
                }
                npc.damage = 40;
                if (NPC.downedMoonlord)
                {
                    npc.damage = 80;
                }
                if (Main.expertMode)
                {
                    npc.damage *= 2;
                }
                if (npc.life > npc.lifeMax * 0.5f || NPC.AnyNPCs(mod.NPCType("JumboCactuar")))
                {
                    npc.direction = (P.Center.X < npc.Center.X ? -1 : 1);
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {
                        npc.velocity.Y = -8;
                    }
                    if (npc.velocity.X * npc.direction < 5.5f)
                    {
                        npc.velocity.X += npc.direction * 0.12f;
                    }
                    if (npc.velocity.X > 5.5f && npc.velocity.Y == 0)
                    {
                        npc.velocity.X = 5.5f;
                    }
                    if (npc.velocity.X < -5.5f && npc.velocity.Y == 0)
                    {
                        npc.velocity.X = -5.5f;
                    }
                    if (npc.velocity.Y != 0)
                    {
                        npc.rotation = npc.ai[1] * 0.0174f * npc.direction;
                        npc.ai[1] += 20;
                    }
                    else
                    {
                        npc.ai[1] = 0;
                    }
                    npc.ai[2]++;
                    if (npc.Distance(P.Center) < 200 && npc.velocity.Y == 0 && npc.ai[2] > 80)
                    {
                        npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (npc.position.Y + npc.height)));
                        npc.velocity.X = 7 * npc.direction;
                        npc.ai[2] = 0;
                    }
                }
                else
                {
                    float speed = 7f;
                    if (npc.Distance(P.Center) < 100 && npc.ai[3] < 200)
                    {
                        npc.ai[3]--;
                        speed = 9f;
                    }
                    npc.direction = (P.Center.X < npc.Center.X ? 1 : -1);
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {
                        npc.velocity.Y = -8;
                    }
                    if (npc.velocity.X * npc.direction < speed)
                    {
                        npc.velocity.X += npc.direction * 0.33f;
                    }
                    if (npc.velocity.Y != 0)
                    {
                        npc.rotation = npc.ai[1] * 0.0174f * npc.direction;
                        npc.ai[1] += 20;
                    }
                    else
                    {
                        npc.ai[1] = 0;
                    }
                }
            }

            if (npc.ai[3] >= 200 && npc.velocity.Y == 0)
            {
                npc.direction = (P.Center.X < npc.Center.X ? -1 : 1);
                npc.velocity.X = 0;
                if (npc.ai[3] >= 210)
                {
                    float speed = 16f;
                    int type = mod.ProjectileType("CactusNeedle2");
                    if (npc.ai[3] % 5 == 0)
                    {
                        Main.PlaySound(2, npc.Center, 7);
                    }
                    Vector2 dir = npc.DirectionTo(P.Center);
                    if (Main.expertMode)
                    {
                        dir = npc.DirectionTo(P.Center + (P.velocity * (npc.Distance(P.Center) / speed)));
                    }
                    Vector2 vel = new Vector2(dir.X, dir.Y).RotatedByRandom(MathHelper.ToRadians(3));
                    dir = vel * speed;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, type, 1, 0f, Main.myPlayer);
                    }
                }
                if ((npc.ai[3] >= 220 && !Main.expertMode) || (Main.expertMode && npc.ai[3] >= 230))
                {
                    npc.ai[3] = 0;
                    npc.velocity.X += npc.direction;
                }
            }

        }
    }
}

