using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class CyclopsMask : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cyclops Mask");
			Tooltip.SetDefault("'Lack of depth perception can be an issue'");
		}
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = 1000;
			item.rare = 3;
			item.vanity = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddIngredient(ItemID.Lens);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}