using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
			Main.npcFrameCount[NPC.type] = 5;
		}
		public override void SetDefaults()
		{
			NPC.width = 200;
			NPC.height = 300;
			NPC.scale = 2f;
			NPC.damage = 150;
			NPC.defense = 30;
			NPC.lifeMax = 300000;
			NPC.boss = true;
			NPC.lavaImmune = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(7, 50, 0, 0);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = 0;
			NPC.frameCounter = 0;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheDecisiveBattle");
			bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("JumboCactuarBag").Type;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
            SceneEffectPriority = SceneEffectPriority.BossMedium;
        }

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale)+1;
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == Mod.Find<ModNPC>("CactusPerrson").Type)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
			NPC.frameCounter++;
			if (NPC.ai[2] <= 0 && !(NPC.ai[1] % 400 >= 380 && NPC.ai[1] < 1500))
			{
				if (NPC.frameCounter >= 8)
				{
					NPC.frameCounter = 0;	
					NPC.frame.Y = (NPC.frame.Y + 384);		
				}
				if (NPC.frame.Y >= 768)
				{
					NPC.frame.Y = 0;	
				}
			}
			if (NPC.ai[2] > 0 && NPC.ai[2] < 15)
			{
				NPC.frame.Y = 768;
			}
			if (NPC.ai[2] >= 15 || (NPC.ai[1] % 400 >= 380 && NPC.ai[1] < 1500))
			{
				if (NPC.frameCounter >= 4)
				{
					NPC.frameCounter = 0;	
					NPC.frame.Y = (NPC.frame.Y + 384);		
				}
				if (NPC.frame.Y >= 1920 || NPC.frame.Y < 1152)
				{
					NPC.frame.Y = 1152;	
				}
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/JumboCactuar1"), NPC.scale);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/GiantNeedle"), NPC.scale);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/GiantNeedle"), NPC.scale);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/GiantNeedle"), NPC.scale);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/JumboCactuar2"), NPC.scale);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/JumboCactuar2"), NPC.scale);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/JumboCactuar2"), NPC.scale);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/JumboCactuar2"), NPC.scale);
			}
		}
		public override void OnKill()
		{
			JoostWorld.downedJumboCactuar = true;

			if (Main.expertMode)
			{
				NPC.DropBossBags();
			}
			else
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Cactustoken").Type, 1 + Main.rand.Next(2));
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DecisiveBattleMusicBox").Type);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("JumboCactuarMask").Type);
                }
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("JumboCactuarTrophy").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FifthAnniversary").Type, 1);
            }
        }
		public override void AI()
		{
			NPC.netUpdate = true;
			Player P = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(false);
				P = Main.player[NPC.target];
            }
            if (!P.active || P.dead)
            {
                if (NPC.timeLeft > 200)
                {
                    NPC.timeLeft = 200;
                }
            }
            else
            {
                NPC.timeLeft = 750;
            }
            bool desert = P.ZoneDesert;
            bool corrupt = P.ZoneCorrupt;
            bool crimson = P.ZoneCrimson;
            bool hallow = P.ZoneHallow;
            if (NPC.velocity.Y == 0 && P.Center.Y < NPC.position.Y)
            {
                NPC.velocity.Y = -20f;
            }
            if (P.position.Y > NPC.position.Y + NPC.height && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.velocity.Y += 0.2f;
                if (NPC.velocity.Y < 0)
                {
                    NPC.velocity.Y = 0;
                }
                if (NPC.velocity.Y > 8f)
                {
                    NPC.velocity.Y = 8f;
                }
            }
            else
            {
                Vector2 pos = new Vector2(NPC.position.X, NPC.position.Y + NPC.height - 64);
                int collide = 0;
                for (int i = 0; i < 16; i++)
                {
                    Vector2 pos2 = new Vector2(NPC.position.X + ((NPC.width / 16) * i), pos.Y);
                    if (Collision.SolidCollision(pos2, NPC.width / 16, 64))
                    {
                        collide++;
                    }
                }
                if (collide > 6)
                {
                    if (NPC.velocity.Y > 0f)
                    {
                        NPC.velocity.Y = 0f;
                        if (P.position.Y < NPC.position.Y - 500)
                        {
                            NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (NPC.position.Y + NPC.height)));
                        }
                    }
                    else
                    {
                        NPC.velocity.Y -= 0.125f;
                        if (NPC.velocity.Y < -3f)
                        {
                            NPC.velocity.Y = -3f;
                        }
                        if (P.position.Y < NPC.position.Y - 400 && NPC.velocity.Y > -8)
                        {
                            NPC.velocity.Y = -8f;
                        }
                    }
                }
                else
                {
                    /*if (npc.velocity.Y == 0)
                    {
                        npc.velocity.Y -= 17.77f;
                    }*/
                    NPC.velocity.Y += 0.5f;
                }
            }
			if (NPC.velocity.Y > 20f)
			{
				NPC.velocity.Y = 20f;
			}
			
			if (NPC.ai[2] <= 0)
			{
				if(P.Center.X < NPC.position.X && NPC.velocity.X > -25.4f)
				{
					NPC.velocity.X -= 0.4f;
				}
				if(P.Center.X > NPC.position.X + NPC.width && NPC.velocity.X < 25.4f) 
				{
					NPC.velocity.X += 0.4f;
				}
				if(NPC.velocity.X < -25.4f)
				{
					NPC.velocity.X = -25.4f;
				}
				if(NPC.velocity.X > 25.4f)
				{
					NPC.velocity.X = 25.4f;
				}
				//npc.position.X += npc.velocity.X;
			}
			if (NPC.ai[2] < 15)
			{
				NPC.ai[1]++;
			}
			
			if (!desert)
			{
				NPC.localAI[0]++;
			}
			else
			{
                NPC.localAI[0] = 0;
			}
			if (NPC.localAI[0] > 300)
			{
				NPC.rotation += 10 * NPC.direction;
				NPC.damage = 300;
                NPC.defense = (int)(NPC.localAI[0] / 2);
				NPC.velocity = NPC.DirectionTo(P.Center) * (NPC.localAI[0] / 20f);
				//npc.noTileCollide = true;
				NPC.ai[3] = 0;
                if (NPC.localAI[3] == 0)
                {
                    Main.NewText("The Jumbo Cactuar enrages as you leave the desert!", Color.DarkOliveGreen);
                    NPC.localAI[3] = 1;
                }
			}
			else
			{
				NPC.rotation = 0;
				NPC.damage = 150;
                NPC.defense = 30;
                //npc.noTileCollide = false;
                if (NPC.ai[2] <= 0 && Vector2.Distance(P.Center, NPC.Center) > 500)
				{
					NPC.ai[3] += 1+Main.rand.Next(3);
					if (Vector2.Distance(P.Center, NPC.Center) > 2000)
					{
						NPC.ai[3] = NPC.ai[3] < 600 ? 600 : 0;
					}
					else if (Vector2.Distance(P.Center, NPC.Center) > 1000)
					{
						NPC.ai[3] += NPC.ai[3] < 600 ? 2 : 0;
					}
				}
				else
				{
					NPC.ai[3] = 0;
				}
				if (NPC.ai[3] >= 600 && NPC.ai[3] < 900)
				{
					Vector2 jumpos = P.Center + new Vector2(0f, -900f);
					NPC.velocity = NPC.DirectionTo(jumpos) * 30;
					//npc.noTileCollide = true;
					if (Math.Abs(NPC.Center.X - jumpos.X) < 150 && Math.Abs(NPC.Center.Y - jumpos.Y) < 200)
					{
						NPC.ai[3] = 900;
					}
				}
				if (NPC.ai[3] >= 1000 && NPC.ai[3] < 1100)
				{
					NPC.velocity = NPC.DirectionTo(P.Center + new Vector2(P.velocity.X * 20, 0f)) * 20;
					//npc.noTileCollide = false;
				}
			}
			if (NPC.ai[3] >= 1100)
			{
				NPC.ai[3] = 0;
			}
			NPC.ai[0]++;
			if (Main.expertMode && NPC.ai[0] >= 700)
			{
				if (Main.rand.Next(4) == 0 && NPC.localAI[2] <= 0)
				{
                    NPC.localAI[2]++;
				}
				if (NPC.localAI[2] > 0)
				{
                    NPC.localAI[2]++;
					if (NPC.localAI[2] % 15 == 0 && Main.netMode != 1)
					{
						int spwn = 700+Main.rand.Next(200);
						if (corrupt)
						{
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("CorruptCactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("CorruptCactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
						else if (crimson)
                        {
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("CrimsonCactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("CrimsonCactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
						else if (hallow)
                        {
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("HallowedCactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("HallowedCactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
						else
                        {
                            int yOff = Main.rand.Next(100);
                            if (!Collision.SolidCollision(new Vector2(P.Center.X - spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X - spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("Cactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                            if (!Collision.SolidCollision(new Vector2(P.Center.X + spwn, P.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                            {
                                NPC.NewNPC((int)P.Center.X + spwn, (int)P.position.Y - yOff, Mod.Find<ModNPC>("Cactuar").Type, 0, 0f, 0f, 0, 0, P.whoAmI);
                            }
                        }
					}
					if (NPC.localAI[2] > 180)
					{
                        NPC.localAI[2] = 0;
                        NPC.ai[0] = 0;
					}
				}
				else
				{
                    if (Main.netMode != 1)
                    {
                        if (NPC.direction == -1)
                        {
                            NPC.NewNPC((int)NPC.position.X - 40, (int)NPC.position.Y - 70, Mod.Find<ModNPC>("GiantNeedle").Type, 0, 0f, 0f, 100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)NPC.position.X, (int)NPC.position.Y - 100, Mod.Find<ModNPC>("GiantNeedle").Type, 0, 0f, 0f, Main.rand.Next(-30, 30), 250f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)NPC.position.X + 40, (int)NPC.position.Y - 110, Mod.Find<ModNPC>("GiantNeedle").Type, 0, 0f, 0f, -100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                        }
                        else
                        {
                            NPC.NewNPC((int)NPC.position.X + 160, (int)NPC.position.Y - 110, Mod.Find<ModNPC>("GiantNeedle").Type, 0, 0f, 0f, 100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)NPC.position.X + 200, (int)NPC.position.Y - 100, Mod.Find<ModNPC>("GiantNeedle").Type, 0, 0f, 0f, Main.rand.Next(-30, 30), 250f + Main.rand.Next(-50, 50), P.whoAmI);
                            NPC.NewNPC((int)NPC.position.X + 240, (int)NPC.position.Y - 70, Mod.Find<ModNPC>("GiantNeedle").Type, 0, 0f, 0f, -100f + Main.rand.Next(-50, 50), 200f + Main.rand.Next(-50, 50), P.whoAmI);
                        }
                    }
                    NPC.ai[0] = 0;
				}
			}
			if (NPC.ai[1] < 1500)
			{
				if (NPC.ai[2] >= 15)
				{
					NPC.ai[1] = 0;
				}
				NPC.ai[2] = 0;
				if (NPC.ai[1] % 400 >= 380)
				{
					float Speed = 8f;
					Vector2 vector8 = new Vector2(NPC.Center.X + (Main.rand.Next(-15, 15) * 15), NPC.Center.Y + (Main.rand.Next(-20, 15) * 15));
					int damage = 10;
					int type = Mod.Find<ModProjectile>("CactusNeedle").Type;
					SoundEngine.PlaySound(SoundID.Item1, NPC.position);
					float rotation = (float)Math.Atan2(vector8.Y - P.Center.Y, vector8.X - P.Center.X);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
				}
			}
			if (NPC.ai[1] >= 1500 && NPC.ai[1] % 17 == 0)
			{
				NPC.ai[2]++;
			}
			if (NPC.ai[2] > 0)
			{
				int b = 255 - (int)(NPC.ai[1] - 1500);
				NPC.color = new Color(255, 255, b);
				NPC.defense = 30 + (int)(NPC.ai[1] - 1500);
				NPC.velocity.X = 0;
				if (NPC.ai[2] < 15)
				{
					NPC.position.X += NPC.ai[1] % (15 - NPC.ai[2]) < (15 - NPC.ai[2])/2 ? 8 : -8;
				}
				else
				{
					NPC.position.X += NPC.ai[1] % 30 < 15 ? 8 : -8;
				}
			}
			if (NPC.ai[2] >= 15)
			{
				float Speed = 8f;
				int damage = 10;
				int type = Mod.Find<ModProjectile>("CactusNeedle").Type;
				SoundEngine.PlaySound(SoundID.Item1, NPC.position);
				float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(NPC.Center.X + (Main.rand.Next(-20, 20) * 20), NPC.Center.Y + (Main.rand.Next(-20, 15) * 30), (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                }
                NPC.ai[1]--;
            }
            NPC.scale = 2;
            if (NPC.timeLeft <= 200)
            {
                NPC.timeLeft--;
                if (NPC.timeLeft < 150)
                {
                    NPC.velocity = new Vector2(0, -2f);
                    NPC.scale = 2 * (NPC.timeLeft / 150f);
                    NPC.rotation = NPC.timeLeft * 6;
                    NPC.direction = -1;
                }
                if (NPC.timeLeft <= 0)
                {
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
        public override bool CheckActive()
        {
            return false;
        }
    }
}

