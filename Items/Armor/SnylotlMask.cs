using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SnylotlMask : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snylotl Mask");
			Tooltip.SetDefault("'HISSsss'");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.value = 1000;
			item.rare = 2;
			item.vanity = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MudBlock, 10);
			recipe.AddIngredient(ItemID.JungleGrassSeeds);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}