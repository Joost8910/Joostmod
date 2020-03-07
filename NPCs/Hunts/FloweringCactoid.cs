using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
	public class FloweringCactoid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flowering Cactoid");
			Main.npcFrameCount[npc.type] = 15;
		}
		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 56;
			npc.damage = 20;
			npc.defense = 12;
            npc.lifeMax = 1000;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0;
			npc.knockBackResist = 0.01f;
			npc.aiStyle = -1;
			npc.frameCounter = 0;
            npc.netAlways = true;
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.player.ZoneBeach && spawnInfo.player.ZoneDesert && spawnInfo.spawnTileY <= Main.worldSurface && !JoostWorld.downedFloweringCactoid && JoostWorld.activeQuest == npc.type && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override void NPCLoot()
        {
            JoostWorld.downedFloweringCactoid = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("FloweringCactoid"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
        int idle = 0;
        public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter++;
			if(idle > 900)
			{
                if (npc.ai[3] > 0 && npc.ai[3] < 15)
                {
                    npc.frame.Y = 464;
                }
                else if (npc.ai[3] >= 15)
                {
                    if (npc.frameCounter >= 8)
                    {
                        npc.frame.Y += 58;
                    }
                    if (npc.frame.Y >= 638 || npc.frame.Y < 522)
                    {
                        npc.frame.Y = 522;
                    }
                }
                else if (npc.ai[1] > 0)
                {
                    if (npc.frameCounter >= 5)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (npc.frame.Y + 58);
                    }
                    if (npc.frame.Y >= 870 || npc.frame.Y < 638)
                    {
                        npc.frame.Y = 638;
                    }
                }
                else
                {
                    if (npc.frameCounter >= 15 / (1 + Math.Abs(npc.velocity.X)))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (npc.frame.Y + 58);
                    }
                    if (npc.frame.Y >= 232)
                    {
                        npc.frame.Y = 0;
                    }
                }
			}
			else
			{
				if (npc.frameCounter >= 6)
				{
					npc.frameCounter = 0;	
					npc.frame.Y = (npc.frame.Y + 58);		
				}
				if (npc.frame.Y >= 464 || npc.frame.Y < 232)
				{
					npc.frame.Y = 232;	
				}
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			npc.ai[0]++;
            if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactite1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactite2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Cactite2"), 1f);
			}
		} 
		int dir = 1;
		public override void AI()
		{
			Player P = Main.player[npc.target];
			npc.netUpdate = true;
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || P.dead || !P.active)
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
                if (idle == 0 && Main.netMode != 1)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int j = 0;
                        for (j = 0; j < 500; j++)
                        {
                            if (!Collision.SolidCollision(npc.position + new Vector2(-125 + (i * 50), -j), npc.width, npc.height))
                            {
                                break;
                            }
                        }
                        if (!Collision.SolidCollision(npc.position + new Vector2(-125 + (i * 50), -j), npc.width, npc.height))
                        {
                            if (i == 2 || i == 3)
                            {
                                NPC.NewNPC((int)npc.position.X - 125 + (i * 50), (int)npc.position.Y - j, mod.NPCType("Cactoid"));
                            }
                            else
                            {
                                NPC.NewNPC((int)npc.position.X - 125 + (i * 50), (int)npc.position.Y - j, mod.NPCType("Cactite"));
                            }
                        }
                    }
                }
				idle += 1 + Main.rand.Next(5);
				if (idle > 900)
				{
					if (npc.velocity.X == 0 && npc.velocity.Y == 0)
					{
						if (Main.rand.Next(4) == 0)
						{
							dir *= -1;
						}
						else
						{
							npc.velocity.Y = -7;
						}
					}
					npc.direction = dir;
					npc.velocity.X = dir * 2;
				}
				if(idle > 2000 && npc.velocity.Y == 0)
				{
					idle = 1;
					npc.velocity.X = 0f;
				}
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                if (Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(npc.Center, P.Center) < 800)
                {
                    npc.ai[0]++;
                }
                npc.noGravity = false;
                npc.noTileCollide = false;
            }
			else
            {
                if (Main.netMode != 1)
                {
                    for (int n = 0; n < 200; n++)
                    {
                        NPC N = Main.npc[n];
                        if (N.Distance(npc.Center) < 1600 && (N.type == mod.NPCType("Cactite") || N.type == mod.NPCType("Cactoid")))
                        {
                            N.ai[2]++;
                        }
                    }
                }
				idle = 1000;
                npc.direction = P.Center.X < npc.Center.X ? -1 : 1;
                if (npc.velocity.X * npc.direction < 6)
                {
                    npc.velocity.X += (float)(npc.direction * 0.1f);
                }
                if (npc.ai[3] < 15)
                {
                    npc.ai[2]++;
                }
                if (npc.ai[1] > 0)
                {
                    npc.noGravity = true;
                    npc.noTileCollide = true;
                    npc.velocity.Y = npc.Center.Y > P.Center.Y ? -3 : 3;
                    npc.rotation = npc.velocity.X * 0.0174f * 1.5f;
                    if (Collision.CanHitLine(npc.position, npc.width, npc.height, P.position, P.width, P.height) && npc.Distance(P.Center) < 100 && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[1] = 0;
                    }
                    if (npc.ai[2] > 1000)
                    {
                        npc.ai[2] = 1000;
                    }
                }
                else
                {
                    npc.rotation = 0;
                    npc.noGravity = false;
                    npc.noTileCollide = false;
                    if (npc.velocity.X == 0 && npc.velocity.Y == 0 && P.position.Y + P.height < npc.position.Y)
                    {
                        npc.velocity.Y = -10;
                    }
                    if (P.position.Y > npc.position.Y + npc.height && npc.ai[3] == 0)
                    {
                        npc.position.Y++;
                    }

                    if (npc.ai[2] <= 1000)
                    {
                        if (npc.ai[3] >= 15)
                        {
                            npc.ai[2] = 0;
                        }
                        npc.ai[3] = 0;
                    }
                    if (npc.ai[2] > 1000 && npc.ai[2] % 17 == 0 && npc.ai[1] < 1 && Main.netMode != 1)
                    {
                        npc.ai[3]++;
                    }
                    if (npc.ai[3] > 0)
                    {
                        int b = 255 - (int)(npc.ai[2] - 1000);
                        npc.color = new Color(255, 255, b);
                        npc.defense = 12 + (int)(npc.ai[2] - 1000);
                        npc.velocity.X = 0;
                        if (npc.ai[3] < 15)
                        {
                            npc.velocity.X = npc.ai[2] % (15 - npc.ai[3]) < (15 - npc.ai[3]) / 2 ? 3 : -3;
                        }
                        else
                        {
                            npc.velocity.X = npc.ai[2] % 30 < 15 ? 3 : -3;
                        }
                    }
                    else if (P.Center.Y < npc.position.Y - 170 || !Collision.CanHitLine(npc.position, npc.width, npc.height, P.position, P.width, P.height) || Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[1]++;
                    }
                    if (npc.ai[3] >= 15)
                    {
                        float Speed = 15f;
                        int type = mod.ProjectileType("CactusNeedle2");
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                        float rotation = (float)Math.Atan2(npc.Center.Y - (P.Center.Y), npc.Center.X - (P.Center.X));
                        float randRot = Main.rand.Next(360);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center.X + (Main.rand.Next(-12, 12)), npc.Center.Y + (Main.rand.Next(-25, 25)), (float)((Math.Cos(randRot) * Speed) * -1), (float)((Math.Sin(randRot) * Speed) * -1), type, 1, 0, Main.myPlayer);
                            if (Main.expertMode)
                            {
                                Projectile.NewProjectile(npc.Center.X + (Main.rand.Next(-12, 12)), npc.Center.Y + (Main.rand.Next(-25, 25)), (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 1, 0, Main.myPlayer);
                            }
                            else
                            {
                                randRot = Main.rand.Next(360);
                                Projectile.NewProjectile(npc.Center.X + (Main.rand.Next(-12, 12)), npc.Center.Y + (Main.rand.Next(-25, 25)), (float)((Math.Cos(randRot) * Speed) * -1), (float)((Math.Sin(randRot) * Speed) * -1), type, 1, 0, Main.myPlayer);
                            }
                        }
                        npc.ai[2]--;
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

