using System;
using System.IO;
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
	public class Pinkzor : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pinkzor");
			Main.npcFrameCount[npc.type] = 5;
		}
		public override void SetDefaults()
		{
			npc.width = 50;
			npc.height = 40;
			npc.damage = 18;
			npc.defense = 4;
			npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0f;
			npc.knockBackResist = 0.1f;
			npc.aiStyle = 1;
			aiType = NPCID.BlueSlime;
			animationType = NPCID.BlueSlime;
			npc.buffImmune[20] = true;
            npc.netAlways = true;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return !spawnInfo.sky && !spawnInfo.playerInTown && Math.Abs(spawnInfo.spawnTileX - Main.spawnTileX) > 500 && spawnInfo.spawnTileY < Main.maxTilesY - 450 && !JoostWorld.downedPinkzor && !NPC.AnyNPCs(npc.type) && !NPC.AnyNPCs(mod.NPCType("Hunt Master")) ? 0.006f : 0f;
		}
		public override void NPCLoot()
		{
			JoostWorld.downedPinkzor = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("Pinkzor"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
		int chance = 1;
        bool regen = false;
		public override void AI()
		{
			Player P = Main.player[npc.target];
            npc.netUpdate = true;
            if (Vector2.Distance(npc.Center, P.Center) > 1500 || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(true);
                P = Main.player[npc.target];
                if (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 1500)
                {
                    regen = true;
                }
            }
            if (!regen)
            {
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                if (Collision.CanHitLine(new Vector2(npc.Center.X, npc.Center.Y), 1, 1, new Vector2(P.Center.X, P.Center.Y), 1, 1) || Vector2.Distance(npc.Center, P.Center) < 400)
                {
                    regen = true;
                }
            }
            if (npc.velocity.Y == 0)
			{
				if (chance != 0)
				{
					chance = Main.rand.Next(150);
				}
				else
				{
					if (Collision.CanHitLine(new Vector2(npc.Center.X, npc.Center.Y), 1, 1, new Vector2(P.Center.X, P.Center.Y), 1, 1))
					{
						npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((P.position.Y-100) - npc.Center.Y));
						npc.velocity.X = (P.Center.X+P.velocity.X - npc.Center.X) / 90;
						chance = 1;
					}
				}
			}
		}
public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Hunt Master"));
                NPC.NewNPC((int)npc.Center.X + 28, (int)npc.Center.Y - 2, NPCID.Pinky);
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 2, NPCID.Pinky);
                NPC.NewNPC((int)npc.Center.X - 28, (int)npc.Center.Y - 2, NPCID.Pinky);
            }
            return true;
        }
		public override bool CheckActive()
		{
			return false;
		}

	}
}

