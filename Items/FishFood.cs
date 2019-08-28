using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class FishFood : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishy Food");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 22;
			item.value = 50;
			item.rare = 1;
			item.bait = 5;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AtlanticCod);
			recipe.SetResult(this, 12);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bass);
			recipe.SetResult(this, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RedSnapper);
			recipe.SetResult(this, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Salmon);
			recipe.SetResult(this, 6);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();

recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Trout);
			recipe.SetResult(this, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Tuna);
			recipe.SetResult(this, 40);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Shrimp);
			recipe.SetResult(this);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();

		}

	}
}

