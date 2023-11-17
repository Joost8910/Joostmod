using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
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
			Main.npcFrameCount[NPC.type] = 15;
		}
		public override void SetDefaults()
		{
			NPC.width = 24;
			NPC.height = 56;
			NPC.damage = 20;
			NPC.defense = 12;
            NPC.lifeMax = 1000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0;
			NPC.knockBackResist = 0.01f;
			NPC.aiStyle = -1;
			NPC.frameCounter = 0;
            NPC.netAlways = true;
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.Player.ZoneBeach && spawnInfo.Player.ZoneDesert && spawnInfo.SpawnTileY <= Main.worldSurface && !JoostWorld.downedFloweringCactoid && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override void OnKill()
        {
            JoostWorld.downedFloweringCactoid = true;
            NPC.DropItemInstanced(NPC.position, NPC.Size, Mod.Find<ModItem>("FloweringCactoid").Type, 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("EvilStone").Type, 1);
            }
        }
        int idle = 0;
        public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
			NPC.frameCounter++;
			if(idle > 900)
			{
                if (NPC.ai[3] > 0 && NPC.ai[3] < 15)
                {
                    NPC.frame.Y = 464;
                }
                else if (NPC.ai[3] >= 15)
                {
                    if (NPC.frameCounter >= 8)
                    {
                        NPC.frame.Y += 58;
                    }
                    if (NPC.frame.Y >= 638 || NPC.frame.Y < 522)
                    {
                        NPC.frame.Y = 522;
                    }
                }
                else if (NPC.ai[1] > 0)
                {
                    if (NPC.frameCounter >= 5)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = (NPC.frame.Y + 58);
                    }
                    if (NPC.frame.Y >= 870 || NPC.frame.Y < 638)
                    {
                        NPC.frame.Y = 638;
                    }
                }
                else
                {
                    if (NPC.frameCounter >= 15 / (1 + Math.Abs(NPC.velocity.X)))
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = (NPC.frame.Y + 58);
                    }
                    if (NPC.frame.Y >= 232)
                    {
                        NPC.frame.Y = 0;
                    }
                }
			}
			else
			{
				if (NPC.frameCounter >= 6)
				{
					NPC.frameCounter = 0;	
					NPC.frame.Y = (NPC.frame.Y + 58);		
				}
				if (NPC.frame.Y >= 464 || NPC.frame.Y < 232)
				{
					NPC.frame.Y = 232;	
				}
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			NPC.ai[0]++;
            if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactite1"), 1f);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactite2"), 1f);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactite2"), 1f);
			}
		} 
		int dir = 1;
		public override void AI()
		{
			Player P = Main.player[NPC.target];
			NPC.netUpdate = true;
            if (Vector2.Distance(NPC.Center, P.Center) > 2500 || NPC.target < 0 || NPC.target == 255 || P.dead || !P.active)
            {
                NPC.TargetClosest(true);
                P = Main.player[NPC.target];
                if (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 2500)
                {
                    NPC.ai[0] = 0;
                }
            }
            if (NPC.ai[0] < 1)
			{
                if (idle == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int j = 0;
                        for (j = 0; j < 500; j++)
                        {
                            if (!Collision.SolidCollision(NPC.position + new Vector2(-125 + (i * 50), -j), NPC.width, NPC.height))
                            {
                                break;
                            }
                        }
                        if (!Collision.SolidCollision(NPC.position + new Vector2(-125 + (i * 50), -j), NPC.width, NPC.height))
                        {
                            if (i == 2 || i == 3)
                            {
                                NPC.NewNPC((int)NPC.position.X - 125 + (i * 50), (int)NPC.position.Y - j, Mod.Find<ModNPC>("Cactoid").Type);
                            }
                            else
                            {
                                NPC.NewNPC((int)NPC.position.X - 125 + (i * 50), (int)NPC.position.Y - j, Mod.Find<ModNPC>("Cactite").Type);
                            }
                        }
                    }
                }
				idle += 1 + Main.rand.Next(5);
				if (idle > 900)
				{
					if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
					{
						if (Main.rand.Next(4) == 0)
						{
							dir *= -1;
						}
						else
						{
							NPC.velocity.Y = -7;
						}
					}
					NPC.direction = dir;
					NPC.velocity.X = dir * 2;
				}
				if(idle > 2000 && NPC.velocity.Y == 0)
				{
					idle = 1;
					NPC.velocity.X = 0f;
				}
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)((float)NPC.lifeMax * 0.001f) : NPC.lifeMax;
                if (Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(NPC.Center, P.Center) < 800)
                {
                    NPC.ai[0]++;
                }
                NPC.noGravity = false;
                NPC.noTileCollide = false;
            }
			else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int n = 0; n < 200; n++)
                    {
                        NPC N = Main.npc[n];
                        if (N.Distance(NPC.Center) < 1600 && (N.type == Mod.Find<ModNPC>("Cactite").Type || N.type == Mod.Find<ModNPC>("Cactoid").Type))
                        {
                            N.ai[2]++;
                        }
                    }
                }
				idle = 1000;
                NPC.direction = P.Center.X < NPC.Center.X ? -1 : 1;
                if (NPC.velocity.X * NPC.direction < 6)
                {
                    NPC.velocity.X += (float)(NPC.direction * 0.1f);
                }
                if (NPC.ai[3] < 15)
                {
                    NPC.ai[2]++;
                }
                if (NPC.ai[1] > 0)
                {
                    NPC.noGravity = true;
                    NPC.noTileCollide = true;
                    NPC.velocity.Y = NPC.Center.Y > P.Center.Y ? -3 : 3;
                    NPC.rotation = NPC.velocity.X * 0.0174f * 1.5f;
                    if (Collision.CanHitLine(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height) && NPC.Distance(P.Center) < 100 && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[1] = 0;
                    }
                    if (NPC.ai[2] > 1000)
                    {
                        NPC.ai[2] = 1000;
                    }
                }
                else
                {
                    NPC.rotation = 0;
                    NPC.noGravity = false;
                    NPC.noTileCollide = false;
                    if (NPC.velocity.X == 0 && NPC.velocity.Y == 0 && P.position.Y + P.height < NPC.position.Y)
                    {
                        NPC.velocity.Y = -10;
                    }
                    if (P.position.Y > NPC.position.Y + NPC.height && NPC.ai[3] == 0)
                    {
                        NPC.position.Y++;
                    }

                    if (NPC.ai[2] <= 1000)
                    {
                        if (NPC.ai[3] >= 15)
                        {
                            NPC.ai[2] = 0;
                        }
                        NPC.ai[3] = 0;
                    }
                    if (NPC.ai[2] > 1000 && NPC.ai[2] % 17 == 0 && NPC.ai[1] < 1 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[3]++;
                    }
                    if (NPC.ai[3] > 0)
                    {
                        int b = 255 - (int)(NPC.ai[2] - 1000);
                        NPC.color = new Color(255, 255, b);
                        NPC.defense = 12 + (int)(NPC.ai[2] - 1000);
                        NPC.velocity.X = 0;
                        if (NPC.ai[3] < 15)
                        {
                            NPC.velocity.X = NPC.ai[2] % (15 - NPC.ai[3]) < (15 - NPC.ai[3]) / 2 ? 3 : -3;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.ai[2] % 30 < 15 ? 3 : -3;
                        }
                    }
                    else if (P.Center.Y < NPC.position.Y - 170 || !Collision.CanHitLine(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height) || Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[1]++;
                    }
                    if (NPC.ai[3] >= 15)
                    {
                        float Speed = 15f;
                        int type = Mod.Find<ModProjectile>("CactusNeedle2").Type;
                        SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                        float rotation = (float)Math.Atan2(NPC.Center.Y - (P.Center.Y), NPC.Center.X - (P.Center.X));
                        float randRot = Main.rand.Next(360);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.Center.X + (Main.rand.Next(-12, 12)), NPC.Center.Y + (Main.rand.Next(-25, 25)), (float)((Math.Cos(randRot) * Speed) * -1), (float)((Math.Sin(randRot) * Speed) * -1), type, 1, 0, Main.myPlayer);
                            if (Main.expertMode)
                            {
                                Projectile.NewProjectile(NPC.Center.X + (Main.rand.Next(-12, 12)), NPC.Center.Y + (Main.rand.Next(-25, 25)), (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 1, 0, Main.myPlayer);
                            }
                            else
                            {
                                randRot = Main.rand.Next(360);
                                Projectile.NewProjectile(NPC.Center.X + (Main.rand.Next(-12, 12)), NPC.Center.Y + (Main.rand.Next(-25, 25)), (float)((Math.Cos(randRot) * Speed) * -1), (float)((Math.Sin(randRot) * Speed) * -1), type, 1, 0, Main.myPlayer);
                            }
                        }
                        NPC.ai[2]--;
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

