using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class TARDISChest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("TARDIS Chest");
			Tooltip.SetDefault("'It's bigger on the inside!'\n" + "'DISCLAIMER: Same size as regular chests.'");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 34;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 27000;
			item.createTile = mod.TileType("TARDISChest");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Sapphire, 2);
			recipe.AddRecipeGroup("IronBar", 2);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}