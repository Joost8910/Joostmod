using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class Cuphead : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cup Mask");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 26;
			item.value = 10000;
			item.rare = 4;
			item.vanity = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ClayBlock, 10);
			recipe.AddIngredient(ItemID.RedDye);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}