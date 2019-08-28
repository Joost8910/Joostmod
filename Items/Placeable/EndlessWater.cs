using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class EndlessWater : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Water Pump");
            Tooltip.SetDefault("'Flood the world!'");
		}

		public override void SetDefaults()
		{
			item.width = 42;
			item.height = 48;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("EndlessWater");
            item.rare = 1;
            item.mech = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottomlessBucket);
            recipe.AddIngredient(ItemID.InletPump);
            recipe.AddIngredient(ItemID.OutletPump);
            recipe.AddIngredient(ItemID.Wire, 2);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}