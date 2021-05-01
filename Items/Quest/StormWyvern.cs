using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class StormWyvern : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Wyvern Soul");
            Tooltip.SetDefault("Quest item for the Hunt Master");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 42;
			item.height = 42;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
