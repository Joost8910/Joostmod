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
			Item.width = 28;
			Item.height = 26;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.vanity = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.ClayBlock, 10)
				.AddIngredient(ItemID.BlueDye)
				.AddTile(TileID.Furnaces)
				.Register();
		}
	}
}