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
			Item.width = 28;
			Item.height = 28;
			Item.vanity = true;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Leather, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}

	}
}