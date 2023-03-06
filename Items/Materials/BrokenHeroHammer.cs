using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Materials
{
	public class BrokenHeroHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Hero Hammer");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.width = 40;
			Item.height = 36;
			Item.value = 100000;
			Item.rare = ItemRarityID.Yellow;
		}

	}
}

