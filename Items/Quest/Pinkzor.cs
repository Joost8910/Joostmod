using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class Pinkzor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Congealed Pink Gel");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

		public override void SetDefaults()
		{
			Item.questItem = true;
			Item.maxStack = 1;
			Item.width = 32;
			Item.height = 30;
			Item.uniqueStack = true;
			Item.rare = ItemRarityID.Quest;		
		}
	}
}
