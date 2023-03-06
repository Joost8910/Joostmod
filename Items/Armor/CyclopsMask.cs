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
			Item.width = 22;
			Item.height = 24;
			Item.value = 1000;
			Item.rare = ItemRarityID.Orange;
			Item.vanity = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 10)
				.AddIngredient(ItemID.Lens)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}