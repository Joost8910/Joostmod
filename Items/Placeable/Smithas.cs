using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class Smithas : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Smithas Sigil");
			Tooltip.SetDefault("Can't really be used for impersonating youtubers, but ehhh whatever.");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 5000;
			item.rare = 9;
			item.createTile = mod.TileType("Smithas");
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(3, 10);
			recipe.AddIngredient(ItemID.Sapphire);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
