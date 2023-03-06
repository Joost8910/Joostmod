using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SleepyMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sleepy Mask");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.vanity = true;

		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Leather, 10)
				.AddIngredient(ItemID.DiamondGemsparkBlock)
				.AddTile(TileID.WorkBenches)
				.Register();
		}

	}
}