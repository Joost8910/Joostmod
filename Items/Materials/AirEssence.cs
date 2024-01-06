using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Materials
{
	public class AirEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Air Essence");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 6));
			ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.width = 22;
			Item.height = 28;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
		}

	}
}

