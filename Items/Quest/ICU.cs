using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Quest
{
	public class ICU : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Quad-retina Eye");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

		public override void SetDefaults()
		{
			Item.questItem = true;
			Item.maxStack = 1;
			Item.width = 36;
			Item.height = 36;
			Item.uniqueStack = true;
			Item.rare = ItemRarityID.Quest;		
		}
	}
}
