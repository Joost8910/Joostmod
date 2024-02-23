using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Legendaries;
using JoostMod.NPCs.Town;

namespace JoostMod.NPCs.Hunts
{
	[AutoloadBossHead]
	public class Pinkzor : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pinkzor");
			Main.npcFrameCount[NPC.type] = 5;
		}
		public override void SetDefaults()
		{
			NPC.width = 50;
			NPC.height = 40;
			NPC.damage = 18;
			NPC.defense = 4;
			NPC.lifeMax = 500;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0f;
			NPC.knockBackResist = 0.1f;
			NPC.aiStyle = 1;
			AIType = NPCID.BlueSlime;
			AnimationType = NPCID.BlueSlime;
			NPC.buffImmune[20] = true;
            NPC.netAlways = true;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return !spawnInfo.Sky && !spawnInfo.PlayerInTown && Math.Abs(spawnInfo.SpawnTileX - Main.spawnTileX) > 500 && spawnInfo.SpawnTileY < Main.maxTilesY - 450 && !JoostWorld.downedPinkzor && !NPC.AnyNPCs(NPC.type) && !NPC.AnyNPCs(ModContent.NPCType<HuntMaster>()) ? 0.006f : 0f;
        }
        public override void OnKill()
        {
            JoostWorld.downedPinkzor = true;
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(NPC, ModContent.ItemType<Items.Quest.Pinkzor>(), Main.rand, 1, 1, 1, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EvilStone>(), 100));
        }
        int chance = 1;
        bool regen = false;
		public override void AI()
		{
			Player P = Main.player[NPC.target];
            NPC.netUpdate = true;
            if (Vector2.Distance(NPC.Center, P.Center) > 1500 || NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
                P = Main.player[NPC.target];
                if (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 1500)
                {
                    regen = true;
                }
            }
            if (!regen)
            {
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)((float)NPC.lifeMax * 0.001f) : NPC.lifeMax;
                if (Collision.CanHitLine(new Vector2(NPC.Center.X, NPC.Center.Y), 1, 1, new Vector2(P.Center.X, P.Center.Y), 1, 1) || Vector2.Distance(NPC.Center, P.Center) < 400)
                {
                    regen = true;
                }
            }
            if (NPC.velocity.Y == 0)
			{
				if (chance != 0)
				{
					chance = Main.rand.Next(150);
				}
				else
				{
					if (Collision.CanHitLine(new Vector2(NPC.Center.X, NPC.Center.Y), 1, 1, new Vector2(P.Center.X, P.Center.Y), 1, 1))
					{
						NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((P.position.Y-100) - NPC.Center.Y));
						NPC.velocity.X = (P.Center.X+P.velocity.X - NPC.Center.X) / 90;
						chance = 1;
					}
				}
			}
		}
		public override bool CheckDead()
        {
			var source = NPC.GetSource_Death();
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HuntMaster>());
                NPC.NewNPC(source, (int)NPC.Center.X + 28, (int)NPC.Center.Y - 2, NPCID.Pinky);
                NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y - 2, NPCID.Pinky);
                NPC.NewNPC(source, (int)NPC.Center.X - 28, (int)NPC.Center.Y - 2, NPCID.Pinky);
            }
            return true;
        }
		public override bool CheckActive()
		{
			return false;
		}

	}
}

