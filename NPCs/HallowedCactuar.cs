using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class HallowedCactuar : ModNPC
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
            npc.lifeMax = 250;
            if (NPC.downedMoonlord)
            {
                npc.damage = 80;
                npc.defense = 36;
                npc.lifeMax = 1000;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 6, 0);
            npc.knockBackResist = 0.75f;
            npc.aiStyle = -1;
            npc.noGravity = false;
            banner = mod.NPCType("Cactuar");
            bannerItem = mod.ItemType("CactuarBanner");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Cactus Person") && !NPC.AnyNPCs(mod.NPCType("JumboCactuar")))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.ai[3] < 135)
            {
                if (npc.velocity.Y < 0)
                {
                    npc.frameCounter += Math.Abs(npc.velocity.Y);
                }
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
        public override void NPCLoot()
        {
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Anniversary"), 1);
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Cactus, 10);
            if (Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LightShard, 1);
                }
            }
            else if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LightShard, 1);
            }

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HallowedCactuar1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HallowedCactuar2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HallowedCactuar2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HallowedCactuar3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HallowedCactuar3"), 1f);
            }
            npc.ai[3]++;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.playerInTown && ((!spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.spawnTileY > Main.worldSurface) && !spawnInfo.player.ZoneBeach && spawnInfo.player.ZoneDesert && spawnInfo.player.ZoneHoly && Main.hardMode ? 0.15f : 0f;
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
            if (((Collision.CanHitLine(npc.Center, 1, 1, P.position, P.width, P.height) && npc.Distance(P.Center) < 550) || npc.ai[3] > 0) && (!cactusPersonNear || npc.ai[3] > 0))
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
                if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                {
                    npc.velocity.Y = -10;
                }
                if (npc.velocity.X * npc.direction < 4)
                {
                    npc.velocity.X += npc.direction * 0.11f;
                }
                if (npc.velocity.X > 4)
                {
                    npc.velocity.X = 4;
                }
                if (npc.velocity.X < -4)
                {
                    npc.velocity.X = -4;
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
            }
            else if (npc.ai[3] < 135)
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
                        npc.velocity.Y = -10;
                    }
                    if (npc.velocity.X * npc.direction < 6.5f)
                    {
                        npc.velocity.X += npc.direction * 0.11f;
                    }
                    if (npc.velocity.X > 6.5f)
                    {
                        npc.velocity.X = 6.5f;
                    }
                    if (npc.velocity.X < -6.5f)
                    {
                        npc.velocity.X = -6.5f;
                    }
                    npc.ai[1]++;
                    if (npc.ai[1] > 80 && npc.velocity.Y == 0)
                    {
                        npc.velocity = npc.DirectionTo(P.Center) * 15;
                        npc.ai[1] = 0;
                    }
                }
                else
                {
                    float speed = 8f;
                    if (npc.Distance(P.Center) < 100 && npc.ai[3] < 135)
                    {
                        npc.ai[3]--;
                        speed = 10f;
                    }
                    npc.direction = (P.Center.X < npc.Center.X ? 1 : -1);
                    if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                    {
                        npc.velocity.Y = -10;
                    }
                    if (npc.velocity.X * npc.direction < speed)
                    {
                        npc.velocity.X += npc.direction * 0.33f;
                    }
                }
            }

            if (npc.ai[3] >= 135)
            {
                npc.direction = (P.Center.X < npc.Center.X ? -1 : 1);
                if (npc.velocity.Y != 0)
                {
                    npc.rotation = npc.ai[3] * 0.0174f * npc.direction * 30;
                }
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
                if (npc.ai[3] >= 145)
                {
                    float speed = 16f;
                    int type = mod.ProjectileType("CactusNeedle2");
                    if (npc.ai[3] % 5 == 0)
                    {
                        Main.PlaySound(2, npc.Center, 7);
                    }
                    Vector2 dir = npc.DirectionTo(P.Center);
                    Vector2 vel = new Vector2(dir.X, dir.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    dir = vel * speed;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, type, 1, 0f, Main.myPlayer);
                    }
                }
                if ((npc.ai[3] >= 155 && !Main.expertMode) || (Main.expertMode && npc.ai[3] >= 165))
                {
                    npc.ai[3] = 0;
                    npc.velocity.X += npc.direction;
                    npc.rotation = 0;
                }
            }
        }
    }
}

