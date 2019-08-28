using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
	public class Spore : ModNPC
    { 
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore");
		}
        public override void SetDefaults()
        {
            npc.width = 10;
            npc.height = 10;
            npc.defense = 0;
            npc.lifeMax = 1;
            if (Main.expertMode)
            {
                npc.damage = 50;
            }
            else
            {
                npc.damage = 25;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath9;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0;
            npc.behindTiles = true;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 39, npc.velocity.X / 10, npc.velocity.Y / 10, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
                }
            }
        }
        public override void AI()
		{
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            if (npc.ai[0] > 2f)
            {
                npc.ai[0] = 2f;
            }
            if (npc.ai[0] < -2f)
            {
                npc.ai[0] = -2f;
            }
            if (P.Center.X < npc.Center.X && npc.ai[0] > 0f)
            {
                npc.ai[0] = 1f;
            }
            if (P.Center.X > npc.Center.X && npc.ai[0] < 0)
            {
                npc.ai[0] = -1f;
            }
            npc.velocity.X = npc.ai[0];
            npc.velocity.Y = npc.ai[1];
            npc.ai[2]++;
            npc.rotation = npc.ai[2] * 0.0174f * 7.2f;
			if (npc.ai[2] % 5 == 0)
			{
				int dust = Dust.NewDust(npc.position, npc.width, npc.height, 39, npc.velocity.X/10, npc.velocity.Y/10, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
			}
			if (npc.ai[1] <= -3)
			{
                npc.ai[1] = 4;
                npc.ai[0] = npc.ai[0] > 0 ? -2 : 2;
			}
			else
			{
                npc.ai[1] -= 0.14f;
			}
		}
	}
}


