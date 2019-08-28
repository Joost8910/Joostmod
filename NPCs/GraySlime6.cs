using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
	public class GraySlime6 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gray Slime");
			Main.npcFrameCount[npc.type] = 5;
		}
		public override void SetDefaults()
		{
			npc.width = 12;
			npc.height = 12;
			npc.damage = 1;
			npc.defense = 1;
			npc.lifeMax = 10;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.knockBackResist = 1f;
			npc.aiStyle = 1;
			aiType = NPCID.BlueSlime;
			animationType = NPCID.BlueSlime;

		}
		public override void NPCLoot()
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, 1);

		}


	}
}

