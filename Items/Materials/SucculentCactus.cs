using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Materials
{
	public class SucculentCactus : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Succulent Cactus");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.width = 14;
			Item.height = 14;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.bait = 20;
		}
	}
}

