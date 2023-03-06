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
			DisplayName.SetDefault("The Knight's Mask");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 26;
			Item.vanity = true;
			Item.value = 10000;
			Item.rare = ItemRarityID.White;
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Marble, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}