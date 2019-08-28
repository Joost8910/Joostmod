using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
		[AutoloadEquip(EquipType.Body)]
	public class GiantBoot : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant Boot");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.vanity = true;
			item.value = 10000;
			item.rare = 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Leather, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}