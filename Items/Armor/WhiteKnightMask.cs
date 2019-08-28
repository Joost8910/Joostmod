using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class WhiteKnightMask : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("White Knight Mask");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.vanity = true;
			item.value = 10000;
			item.rare = 0;
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Marble, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}