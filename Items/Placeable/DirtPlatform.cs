using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class DirtPlatform : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dirt Platform");
            Tooltip.SetDefault("Places at insane speeds");
		}

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 10;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 3;
			Item.useTime = 2;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.DirtPlatform>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(2)
				.AddIngredient(ItemID.DirtBlock)
				.Register();
		}
	}
}