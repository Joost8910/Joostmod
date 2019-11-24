using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class AncientStone : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Stone");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("AncientStone");
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EarthEssence"));
            recipe.AddTile(mod.TileType("ShrineOfLegends"));
            recipe.SetResult(this, 99);
            recipe.AddRecipe();
        }
    }
}