using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class SporeSpawn : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore Mother");
            Main.npcFrameCount[npc.type] = 3;
		}
		public override void SetDefaults()
		{
			npc.width = 50;
			npc.height = 50;
			npc.damage = 30;
			npc.defense = 14;
			npc.lifeMax = 1500;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0;
			npc.knockBackResist = 0;
			npc.aiStyle = -1;
			npc.noGravity = true;
            npc.netAlways = true;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneJungle && spawnInfo.spawnTileY > (Main.worldSurface + Main.rockLayer) / 2 && !JoostWorld.downedSporeSpawn && JoostWorld.activeQuest == npc.type && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return npc.ai[0] > 0 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void NPCLoot()
		{
            JoostWorld.downedSporeSpawn = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("SporeSpawn"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
		public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0]++;
            if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SporeSpawn"), 1f);
				Gore.NewGore(npc.position, -npc.velocity, mod.GetGoreSlot("Gores/SporeSpawn"), 1f);
			}
        }
        Vector2 posOff = new Vector2(0, -100);
        public override void AI()
		{
            Player P = Main.player[npc.target];
            npc.netUpdate = true;
            npc.rotation += 0.0174f * 7.2f * npc.direction;
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
                P = Main.player[npc.target];
                if (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 2500)
                {
                    npc.ai[0] = 0;
                }
            }
            if (npc.ai[0] < 1)
            {
                npc.velocity *= 0;
                npc.ai[1] = 810;
                npc.ai[2] = 0;
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)(npc.lifeMax * 0.001f) : npc.lifeMax;
                if (Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(npc.Center, P.Center) < 400)
                {
                    npc.ai[0]++;
                }   
            }
            else
            {
                npc.ai[1]++;
                npc.direction = npc.velocity.X > 0 ? -1 : 1;
                if (Main.netMode != 1)
                {
                    if (npc.ai[1] % 50 == 0)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Spore"), 0, npc.whoAmI, 2f, 4);
                    }
                    if (npc.ai[1] % 50 == 25)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Spore"), 0, npc.whoAmI, -2f, 4);
                    }
                }
                npc.defense = 10;
                if (npc.ai[1] > 500)
                {
                    if (npc.ai[1] >= 818)
                    {
                        npc.ai[1] = 0;
                    }
                    if (npc.ai[1] < 510 || npc.ai[1] > 810)
                    {
                        npc.velocity = Vector2.Zero;
                    }
                    else
                    {
                        npc.defense = 25;
                        if (npc.ai[1] == 510)
                        {
                            npc.velocity = npc.DirectionTo(P.Center) * 10;
                        }
                        else
                        {
                            if (npc.velocity.X != npc.oldVelocity.X)
                            {
                                npc.velocity.X = -npc.oldVelocity.X;
                                Main.PlaySound(3, npc.Center, 3);
                            }
                            if (npc.velocity.Y != npc.oldVelocity.Y)
                            {
                                npc.velocity.Y = -npc.oldVelocity.Y;
                                Main.PlaySound(3, npc.Center, 3);
                            }
                        }
                    }
                }
                else
                {
                    Vector2 targetPos = P.Center + posOff;
                    npc.velocity = npc.DirectionTo(targetPos) * 5;
                    if (npc.Distance(targetPos) < 25)
                    {
                        npc.ai[2]++;
                        if (npc.ai[2] >= 8)
                        {
                            npc.ai[2] = 0;
                        }

                        if (npc.ai[2] == 0 || npc.ai[2] == 4)
                        {
                            posOff = new Vector2(0, -100);
                        }
                        if (npc.ai[2] == 1)
                        {
                            posOff = new Vector2(100, -50);
                        }
                        if (npc.ai[2] == 2)
                        {
                            posOff = new Vector2(150, -100);
                        }
                        if (npc.ai[2] == 3)
                        {
                            posOff = new Vector2(100, -150);
                        }
                        if (npc.ai[2] == 5)
                        {
                            posOff = new Vector2(-100, -50);
                        }
                        if (npc.ai[2] == 6)
                        {
                            posOff = new Vector2(-150, -100);
                        }
                        if (npc.ai[2] == 7)
                        {
                            posOff = new Vector2(100, -150);
                        }
                    }
                }
            }
		}
        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[1] <= 500)
            {
                npc.frame.Y = 0;
            }
            else if ((npc.ai[1] > 500 && npc.ai[1] < 510) || npc.ai[1] > 810)
            {
                npc.frame.Y = 70;
            }
            else
            {
                npc.frame.Y = 140;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}

