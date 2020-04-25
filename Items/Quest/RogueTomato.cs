using Terraria;
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
			item.questItem = true;
			item.maxStack = 1;
			item.width = 32;
			item.height = 36;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
