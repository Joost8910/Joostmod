using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class ImpLord : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Imp Tail");
            Tooltip.SetDefault("Quest item for the Hunt Master");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

		public override void SetDefaults()
		{
			Item.questItem = true;
			Item.maxStack = 1;
			Item.width = 42;
			Item.height = 42;
			Item.uniqueStack = true;
			Item.rare = ItemRarityID.Quest;		
		}
	}
}
