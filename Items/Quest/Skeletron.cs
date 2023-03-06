using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class Skeletron : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Big Bone");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

		public override void SetDefaults()
		{
			Item.questItem = true;
			Item.maxStack = 1;
			Item.width = 50;
			Item.height = 32;
			Item.uniqueStack = true;
			Item.rare = ItemRarityID.Quest;		
		}
	}
}
