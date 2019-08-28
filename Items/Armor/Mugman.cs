using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class Mugman : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mug Mask");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 26;
			item.value = 10000;
			item.rare = 1;
			item.vanity = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ClayBlock, 10);
			recipe.AddIngredient(ItemID.BlueDye);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}