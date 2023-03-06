using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class TARDISChest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("TARDIS Chest");
			Tooltip.SetDefault("'It's bigger on the inside!'\n" + "DISCLAIMER: Same size as regular chests");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 34;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 27000;
			Item.createTile = Mod.Find<ModTile>("TARDISChest").Type;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
.AddIngredient(ItemID.Sapphire, 2)
.AddRecipeGroup("IronBar", 2)
.Register();

		}
	}
}