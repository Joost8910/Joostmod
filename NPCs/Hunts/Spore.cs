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
            NPC.width = 10;
            NPC.height = 10;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            if (Main.expertMode)
            {
                NPC.damage = 50;
            }
            else
            {
                NPC.damage = 25;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath9;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0;
            NPC.behindTiles = true;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }
        public override bool PreKill()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 39, NPC.velocity.X / 10, NPC.velocity.Y / 10, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
                }
            }
        }
        public override void AI()
		{
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            if (NPC.ai[0] > 2f)
            {
                NPC.ai[0] = 2f;
            }
            if (NPC.ai[0] < -2f)
            {
                NPC.ai[0] = -2f;
            }
            if (P.Center.X < NPC.Center.X && NPC.ai[0] > 0f)
            {
                NPC.ai[0] = 1f;
            }
            if (P.Center.X > NPC.Center.X && NPC.ai[0] < 0)
            {
                NPC.ai[0] = -1f;
            }
            NPC.velocity.X = NPC.ai[0];
            NPC.velocity.Y = NPC.ai[1];
            NPC.ai[2]++;
            NPC.rotation = NPC.ai[2] * 0.0174f * 7.2f;
			if (NPC.ai[2] % 5 == 0)
			{
				int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 39, NPC.velocity.X/10, NPC.velocity.Y/10, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
			}
			if (NPC.ai[1] <= -3)
			{
                NPC.ai[1] = 4;
                NPC.ai[0] = NPC.ai[0] > 0 ? -2 : 2;
			}
			else
			{
                NPC.ai[1] -= 0.14f;
			}
		}
	}
}


