using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class RogueTomato : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tomato Head");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

		public override void SetDefaults()
		{
			Item.questItem = true;
			Item.maxStack = 1;
			Item.width = 32;
			Item.height = 36;
			Item.uniqueStack = true;
			Item.rare = ItemRarityID.Quest;		
		}
	}
}
