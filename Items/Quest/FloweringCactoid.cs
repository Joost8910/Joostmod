using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Quest
{
	public class FloweringCactoid : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactoid Flower");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

		public override void SetDefaults()
		{
			Item.questItem = true;
			Item.maxStack = 1;
			Item.width = 18;
			Item.height = 16;
			Item.uniqueStack = true;
			Item.rare = ItemRarityID.Quest;		
		}
	}
}
