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
			npc.width = 26;
			npc.height = 72;
			npc.damage = 80;
			npc.defense = 50;
			npc.lifeMax = 750;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0f;
			npc.knockBackResist = 0f;
			npc.aiStyle = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
		}
				public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GiantNeedle"), npc.scale);
			}

		}	
		
		public override void AI()
		{
			npc.ai[0]++;
			Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(true);
				npc.velocity = new Vector2(0f, -200f);
				npc.noTileCollide = true;
				if (npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
				return;
			}
			npc.netUpdate = true;
			/*if(npc.Center.X < P.Center.X)
			{
				npc.velocity.X = 10f;
			}
			else
			{
				npc.velocity.X = -10f;
			}*/

			npc.ai[1] += 1;
			
			if (npc.ai[1] >= 200)
			{
				npc.ai[1] = 0;
			}
			if (npc.ai[1] % 100 < 85 && npc.ai[1] % 100 > 15)
			{
				npc.rotation = npc.ai[1] / 2;
			}
			else
			{
				npc.rotation = (P.Center - npc.Center).ToRotation() + (90 * 0.0174f);
			}
			if (npc.ai[1] >= 100)
			{
				npc.velocity = npc.DirectionTo(P.Center + new Vector2(npc.ai[2], npc.ai[3])) * 20;
			}
			else
			{
				npc.velocity = npc.DirectionTo(P.Center + new Vector2(-npc.ai[2], -npc.ai[3])) * 20;
			}
		}
	}
}

