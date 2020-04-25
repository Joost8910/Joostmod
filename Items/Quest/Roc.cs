using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class Roc : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Large Wings");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 94;
			item.height = 56;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
