using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
	public class GraySlime6 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gray Slime");
			Main.npcFrameCount[NPC.type] = 5;
		}
		public override void SetDefaults()
		{
			NPC.width = 12;
			NPC.height = 12;
			NPC.damage = 1;
			NPC.defense = 1;
			NPC.lifeMax = 10;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 1, 0);
            NPC.knockBackResist = 1f;
			NPC.aiStyle = 1;
			AIType = NPCID.BlueSlime;
			AnimationType = NPCID.BlueSlime;

        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1));
        }

    }
}

