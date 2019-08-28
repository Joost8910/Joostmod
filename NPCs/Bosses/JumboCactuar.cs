using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
	[AutoloadBossHead]
	public class JumboCactuar : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jumbo Cactuar");
			Main.npcFrameCount[npc.type] = 5;
		}
		public override void SetDefaults()
		{
			npc.width = 200;
			npc.height = 300;
			npc.scale = 2f;
			npc.damage = 150;
			npc.defense = 20;
			npc.lifeMax = 300000;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = Item.buyPrice(10, 0, 0, 0);
			npc.knockBackResist = 0f;
			npc.aiStyle = 0;
			npc.frameCounter = 0;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheDecisiveBattle");
			bossBag = mod.ItemType("JumboCactuarBag");
			npc.noTileCollide = true;
			npc.noGravity = true;
            musicPriority = MusicPriority.BossMedium;
        }

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale)+1;
			npc.damage = (int)(npc.damage * 0.7f);
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("CactusPerrson"))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter++;
			if (npc.ai[2] <= 0)
			{
				if (npc.frameCounter >= 8)
				{
					npc.frameCounter = 0;	
					npc.frame.Y = (npc.frame.Y + 384);		
				}
				if (npc.frame.Y >= 768)
				{
					npc.frame.Y = 0;	
				}
			}
			if (npc.ai[2] > 0 && npc.ai[2] < 15)
			{
				npc.frame.Y = 768;
			}
			if (npc.ai[2] >= 15 || (npc.ai[1] % 400 >= 380 && npc.ai[1] < 1500))
			{
				if (npc.frameCounter >= 8)
				{
					npc.frameCounter = 0;	
					npc.frame.Y = (npc.frame.Y + 384);		
				}
				if (npc.frame.Y >= 1920)
				{
					npc.frame.Y = 1152;	
				}
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JumboCactuar1"), npc.scale);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantNeedle"), npc.scale);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantNeedle"), npc.scale);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantNeedle"), npc.scale);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JumboCactuar2"), npc.scale);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JumboCactuar2"), npc.scale);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JumboCactuar2"), npc.scale);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JumboCactuar2"), npc.scale);
			}
		}
		public override void NPCLoot()
		{
			JoostWorld.downedJumboCactuar = true;

			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Cactustoken"), 1 + Main.rand.Next(2));
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DecisiveBattleMusicBox"));
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JumboCactuarMask"));
                }
                if (Main.rand.Next(5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JumboCactuarTrophy"));
                }
			}
		}
		public override void AI()
		{
			npc.ai[0]++;
			npc.netUpdate = true;
			Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(false);
				P = Main.player[npc.target];
            }
            if (!P.active || P.dead)
            {
                if (npc.timeLeft > 200)
                {
                    npc.timeLeft = 200;
                }
            }
            else
            {
                npc.timeLeft = 750;
            }
            bool desert = P.ZoneDesert;
            bool corrupt = P.ZoneCorrupt;
            bool crimson = P.ZoneCrimson;
            bool hallow = P.ZoneHoly;
            if (npc.velocity.Y == 0 && P.Center.Y < npc.position.Y)
            {
                npc.velocity.Y = -20f;
            }
            if (P.position.Y > npc.position.Y + npc.height && Collision.SolidCollision(npc.position, npc.width, npc.height))
            {
                npc.velocity.Y += 0.2f;
                if (npc.velocity.Y < 0)
                {
                    npc.velocity.Y = 0;
                }
                if (npc.velocity.Y > 6f)
                {
                    npc.velocity.Y = 6f;
                }
            }
            else
            {
                Vector2 pos = new Vector2(npc.position.X, npc.position.Y + npc.height - 64);
                int collide = 0;
                for (int i = 0; i < 16; i++)
                {
                    Vector2 pos2 = new Vector2(npc.position.X + ((npc.width / 16) * i), pos.Y);
                    if (Collision.SolidCollision(pos2, npc.width / 16, 64))
                    {
                        collide++;
                    }
                }
                if (collide > 6)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = 0f;
                    }
                    npc.velocity.Y -= 0.125f;
                    if (npc.velocity.Y < -2f)
                    {
                        npc.velocity.Y = -2f;
                    }
                }
                else
                {
                    if (npc.velocity.Y == 0)
                    {
                        npc.velocity.Y -= 17.77f;
                    }
                    npc.velocity.Y += 0.5f;
                }
            }
			if (npc.velocity.Y > 15f)
			{
				npc.velocity.Y = 15f;
			}
			
			if (npc.ai[2] <= 0)
			{
				if(P.Center.X < npc.position.X && npc.velocity.X > -25.4f)
				{
					npc.velocity.X -= 0.4f;
				}
				if(P.Center.X > npc.position.X + npc.width && npc.velocity.X < 25.4f) 
				{
					npc.velocity.X += 0.4f;
				}
				if(npc.velocity.X < -25.4f)
				{
					npc.velocity.X = -25.4f;
				}
				if(npc.velocity.X > 25.4f)
				{
					npc.velocity.X = 25.4f;
				}
				//npc.position.X += npc.velocity.X;
			}
			if (npc.ai[2] < 15)
			{
				npc.ai[1]++;
			}
			
			if (!desert)
			{
				npc.localAI[0]++;
			}
			else
			{
                npc.localAI[0] = 0;
			}
			if (npc.localAI[0] > 300)
			{
				npc.rotation += 10 * npc.direction;
				npc.damage = 300;
                npc.defense = 150;
				npc.velocity = npc.DirectionTo(P.Center) * 15;
				//npc.noTileCollide = true;
				npc.ai[3] = 0;
                if (npc.localAI[3] == 0)
                {
                    Main.NewText("The Jumbo Cactuar enrages as you leave the desert!", Color.DarkOliveGreen);
                    npc.localAI[3] = 1;
                }
			}
			else
			{
				npc.rotation = 0;
				npc.damage = 150;
                npc.defense = 30;
                //npc.noTileCollide = false;
                if (npc.ai[2] <= 0 && Vector2.Distance(P.Center, npc.Center) > 500)
				{
					npc.ai[3] += 1+Main.rand.Next(3);
					if (Vector2.Distance(P.Center, npc.Center) > 2000)
					{
						npc.ai[3] = npc.ai[3] < 600 ? 600 : 0;
					}
					else if (Vector2.Distance(P.Center, npc.Center) > 1000)
					{
						npc.ai[3] += npc.ai[3] < 600 ? 2 : 0;
					}
				}
				else
				{
					npc.ai[3] = 0;
				}
				if (npc.ai[3] >= 600 && npc.ai[3] < 900)
				{
					Vector2 jumpos = P.Center + new Vector2(0f, -900f);
					npc.velocity = npc.DirectionTo(jumpos) * 30;
					//npc.noTileCollide = true;
					if (Math.Abs(npc.Center.X - jumpos.X) < 150 && Math.Abs(npc.Center.Y - jumpos.Y) < 200)
					{
						npc.ai[3] = 900;
					}
				}
				if (npc.ai[3] >= 1000 && npc.ai[3] < 1100)
				{
					npc.velocity = npc.DirectionTo(P.Center + new Vector2(P.velocity.X * 20, 0f)) * 20;
					//npc.noTileCollide = false;
				}
			}
			if (npc.ai[3] >= 1100)
			{
				npc.ai[3] = 0;
			}
			npc.localAI[1]++;
			if (Main.expertMode && npc.localAI[1] >= 700)
			{
				if (Main.rand.Next(4) == 0 && npc.localAI[2] <= 0)
				{
                    npc.localAI[2]++;
				}
				if (npc.localAI[2] > 0)
				{
                    npc.localAI[2]++;
					if (npc.localAI[2] % 15 == 0 && Main.netMode != 1)
					{
						int spwn = 700+Main.rand.Next(200);
						if (corrupt)
						{
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, mod.NPCType("CorruptCactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, mod.NPCType("CorruptCactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
						else if (crimson)
                        {
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, mod.NPCType("CrimsonCactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, mod.NPCType("CrimsonCactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
						else if (hallow)
                        {
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, mod.NPCType("HallowedCactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, mod.NPCType("HallowedCactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
						else
                        {
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, mod.NPCType("Cactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, mod.NPCType("Cactuar"), 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
					}
					if (npc.localAI[2] > 180)
					{
                        npc.localAI[2] = 0;
                        npc.localAI[1] = 0;
					}
				}
				else
				{
                    if (Main.netMode != 1)
                    {
                        if (npc.direction == -1)
                        {
                            NPC.NewNPC((int)npc.position.X - 40, (int)npc.position.Y - 70, mod.NPCType("GiantNeedle"), 0, 0f, 0f, 100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y - 100, mod.NPCType("GiantNeedle"), 0, 0f, 0f, Main.rand.Next(-30, 30), 250f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)npc.position.X + 40, (int)npc.position.Y - 110, mod.NPCType("GiantNeedle"), 0, 0f, 0f, -100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                        }
                        else
                        {
                            NPC.NewNPC((int)npc.position.X + 160, (int)npc.position.Y - 110, mod.NPCType("GiantNeedle"), 0, 0f, 0f, 100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)npc.position.X + 200, (int)npc.position.Y - 100, mod.NPCType("GiantNeedle"), 0, 0f, 0f, Main.rand.Next(-30, 30), 250f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)npc.position.X + 240, (int)npc.position.Y - 70, mod.NPCType("GiantNeedle"), 0, 0f, 0f, -100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                        }
                    }
                    npc.localAI[1] = 0;
				}
			}
			if (npc.ai[1] < 1500)
			{
				if (npc.ai[2] >= 15)
				{
					npc.ai[1] = 0;
				}
				npc.ai[2] = 0;
				if (npc.ai[1] % 400 >= 380)
				{
					float Speed = 8f;
					Vector2 vector8 = new Vector2(npc.Center.X + (Main.rand.Next(-15, 15) * 15), npc.Center.Y + (Main.rand.Next(-20, 15) * 15));
					int damage = 10;
					int type = mod.ProjectileType("CactusNeedle");
					Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
					float rotation = (float)Math.Atan2(vector8.Y - P.Center.Y, vector8.X - P.Center.X);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
				}
			}
			if (npc.ai[1] >= 1500 && npc.ai[1] % 17 == 0)
			{
				npc.ai[2]++;
			}
			if (npc.ai[2] > 0)
			{
				int b = 255 - (int)(npc.ai[1] - 1500);
				npc.color = new Color(255, 255, b);
				npc.defense = 30 + (int)(npc.ai[1] - 1500);
				npc.velocity.X = 0;
				if (npc.ai[2] < 15)
				{
					npc.position.X += npc.ai[1] % (15 - npc.ai[2]) < (15 - npc.ai[2])/2 ? 8 : -8;
				}
				else
				{
					npc.position.X += npc.ai[1] % 30 < 15 ? 8 : -8;
				}
			}
			if (npc.ai[2] >= 15)
			{
				float Speed = 8f;
				int damage = 10;
				int type = mod.ProjectileType("CactusNeedle");
				Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
				float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(npc.Center.X + (Main.rand.Next(-20, 20) * 20), npc.Center.Y + (Main.rand.Next(-20, 15) * 30), (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                }
                npc.ai[1]--;
            }
            npc.scale = 2;
            if (npc.timeLeft <= 200)
            {
                npc.timeLeft--;
                if (npc.timeLeft < 150)
                {
                    npc.velocity = new Vector2(0, -2f);
                    npc.scale = 2 * (npc.timeLeft / 150f);
                    npc.rotation = npc.timeLeft * 6;
                    npc.direction = -1;
                }
                if (npc.timeLeft <= 0)
                {
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
        public override bool CheckActive()
        {
            return false;
        }
    }
}

