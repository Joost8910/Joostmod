using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class Fury : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Furious Forging");
			Tooltip.SetDefault("Can't really be used for impersonating youtubers, but ehhh whatever.");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 5000;
			Item.rare = ItemRarityID.Cyan;
			Item.createTile = Mod.Find<ModTile>("Fury").Type;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("Wood", 10)
			.AddRecipeGroup("IronBar")
			.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}
