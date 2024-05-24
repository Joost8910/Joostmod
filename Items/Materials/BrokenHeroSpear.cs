using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Materials
{
	public class BrokenHeroSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broken Hero Spear");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 9999;
			Item.width = 58;
			Item.height = 38;
			Item.value = 100000;
			Item.rare = ItemRarityID.Yellow;
		}

	}
}

