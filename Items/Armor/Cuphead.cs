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
			Item.width = 30;
			Item.height = 26;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.vanity = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.ClayBlock, 10)
				.AddIngredient(ItemID.RedDye)
				.AddTile(TileID.Furnaces)
				.Register();

		}
	}
}