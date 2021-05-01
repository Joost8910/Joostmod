using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class RogueTomato : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rogue Tomato");
            Main.npcFrameCount[npc.type] = 10;
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 48;
            npc.damage = 20;
            npc.defense = 8;
            npc.lifeMax = 650;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0;
            npc.knockBackResist = 0.01f;
            npc.aiStyle = -1;
            npc.frameCounter = 0;
            npc.noTileCollide = false;
            npc.noGravity = false;
            npc.netAlways = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return Main.dayTime && !spawnInfo.sky && spawnInfo.spawnTileY <= Main.worldSurface && !JoostWorld.downedRogueTomato && JoostWorld.activeQuest.Contains(npc.type) && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frame.Y = (int)(npc.ai[0] * 2);
        }
        public override void NPCLoot()
        {
            JoostWorld.downedRogueTomato = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("RogueTomato"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0] += npc.ai[0] < 1 ? 1 : 0;
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RogueTomato"), 1f);
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return npc.ai[0] > 20 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            npc.netUpdate = true;
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || P.dead || !P.active)
            {
                npc.TargetClosest(true);
                P = Main.player[npc.target];
                if (npc.ai[0] >= 20 && (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 2500))
                {
                    npc.ai[0] = 19;
                    npc.ai[3] = 1;
                }
            }
            if (npc.ai[0] < 1)
            {
                npc.noTileCollide = false;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                npc.velocity.X = 0;
                npc.rotation = 0;
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                if (Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(npc.Center, P.Center) < 400 && !P.dead && P.active)
                {
                    npc.ai[0]++;
                }
            }
            else if (npc.ai[0] < 20)
            {
                if (npc.ai[3] == 1)
                {
                    npc.ai[0]--;
                }
                else
                {
                    npc.ai[0]++;
                }
            }
            else
            {
                if (npc.velocity.Y == 0)
                {
                    npc.direction = P.Center.X < npc.Center.X ? -1 : 1;
                }
                if (npc.velocity.X == 0 && npc.velocity.Y >= 0 && P.position.Y + P.height < npc.position.Y + npc.height)
                {
                    if (npc.velocity.Y > 0)
                    {
                        npc.velocity.X = -npc.direction;
                    }
                    npc.velocity.Y = -6;
                    npc.noTileCollide = true;
                }
                if (npc.velocity.X * npc.direction < 4)
                {
                    npc.velocity.X += npc.direction * 0.081f;
                }
                if (npc.velocity.X > 4 && npc.velocity.Y == 0)
                {
                    npc.velocity.X = 4;
                }
                if (npc.velocity.X < -4 && npc.velocity.Y == 0)
                {
                    npc.velocity.X = -4;
                }
                if (npc.velocity.Y > 0)
                {
                    npc.noTileCollide = false;
                }
                if (P.position.Y > npc.position.Y + npc.height && Math.Abs(npc.Center.X - P.Center.X) < 100)
                {
                    npc.noTileCollide = true;
                }

                if (npc.velocity.Y != 0)
                {
                    npc.ai[0] = 260;
                    npc.rotation += npc.direction * 24 * 0.0174f;
                }
                else
                {
                    npc.rotation = 0;
                    npc.ai[1]++;
                    if (npc.ai[1] > 6)
                    {
                        npc.ai[1] = 0;
                        npc.ai[0] += 30;
                    }
                    if (npc.ai[0] > 200)
                    {
                        npc.ai[0] = 50;
                    }
                    if (npc.ai[0] == 140)
                    {
                        if (npc.soundDelay <= 0)
                        {
                            npc.soundDelay = 30;
                            Main.PlaySound(0, (int)npc.Center.X, (int)npc.Center.Y, -1, 0.275f * npc.scale, 0.3f - (0.5f * (npc.scale - 1)));
                        }
                        npc.position.X += npc.direction * 8;
                        npc.direction = npc.velocity.X > 0 ? 1 : -1;
                    }
                    else if (npc.velocity.X * npc.direction <= 0)
                    {
                        npc.ai[0] = 230;
                        Vector2 posi = new Vector2(npc.Center.X - npc.direction * 12, npc.position.Y + npc.height + 4);
                        Point pos = posi.ToTileCoordinates();
                        Tile tile = Framing.GetTileSafely(pos.X, pos.Y);
                        if (tile.active())
                        {
                            Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tile)];
                            dust.velocity.Y = -2.5f;
                            dust.velocity.X = npc.velocity.X * 2.5f;
                        }
                    }
                    else if (npc.position.Y > P.position.Y + P.height)
                    {
                        npc.ai[2]++;
                        if (npc.ai[2] > 15)
                        {
                            npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (npc.position.Y + npc.height)));
                            npc.noTileCollide = true;
                            npc.ai[2] = 0;
                        }
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}

