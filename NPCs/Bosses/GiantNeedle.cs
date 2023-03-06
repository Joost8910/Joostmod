using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
	public class GiantNeedle : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant Needle");
		}
		public override void SetDefaults()
		{
			NPC.width = 26;
			NPC.height = 72;
			NPC.damage = 80;
			NPC.defense = 50;
			NPC.lifeMax = 750;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0f;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = 0;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
		}
				public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/GiantNeedle"), NPC.scale);
			}

		}	
		
		public override void AI()
		{
			NPC.ai[0]++;
			Player P = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
				NPC.velocity = new Vector2(0f, -200f);
				NPC.noTileCollide = true;
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
				return;
			}
			NPC.netUpdate = true;
			/*if(npc.Center.X < P.Center.X)
			{
				npc.velocity.X = 10f;
			}
			else
			{
				npc.velocity.X = -10f;
			}*/

			NPC.ai[1] += 1;
			
			if (NPC.ai[1] >= 200)
			{
				NPC.ai[1] = 0;
			}
			if (NPC.ai[1] % 100 < 85 && NPC.ai[1] % 100 > 15)
			{
				NPC.rotation = NPC.ai[1] / 2;
			}
			else
			{
				NPC.rotation = (P.Center - NPC.Center).ToRotation() + (90 * 0.0174f);
			}
			if (NPC.ai[1] >= 100)
			{
				NPC.velocity = NPC.DirectionTo(P.Center + new Vector2(NPC.ai[2], NPC.ai[3])) * 20;
			}
			else
			{
				NPC.velocity = NPC.DirectionTo(P.Center + new Vector2(-NPC.ai[2], -NPC.ai[3])) * 20;
			}
		}
	}
}

