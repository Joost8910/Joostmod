//Should I remove this item or make it less simple to obtain? Currently makes early game fishing really cheap
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
{
	public class FishFood : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishy Food");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.width = 14;
			Item.height = 22;
			Item.value = 50;
			Item.rare = ItemRarityID.Blue;
			Item.bait = 5;
		}
		public override void AddRecipes()
		{
			CreateRecipe(12)
				.AddIngredient(ItemID.AtlanticCod)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe(2)
				.AddIngredient(ItemID.Bass)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe(3)
				.AddIngredient(ItemID.RedSnapper)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe(6)
				.AddIngredient(ItemID.Salmon)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe(3)
				.AddIngredient(ItemID.Trout)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe(40)
				.AddIngredient(ItemID.Tuna)
				.AddTile(TileID.WorkBenches)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.Shrimp)
				.AddTile(TileID.WorkBenches)
				.Register();

		}

	}
}

