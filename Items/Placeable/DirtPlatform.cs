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
			item.width = 8;
			item.height = 10;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 3;
			item.useTime = 2;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("DirtPlatform");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock);
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
	}
}