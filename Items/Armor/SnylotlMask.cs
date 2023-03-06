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
			Item.width = 24;
			Item.height = 26;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.vanity = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MudBlock, 10)
				.AddIngredient(ItemID.JungleGrassSeeds)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}