using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class Smithas : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Smithas Sigil");
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
			Item.createTile = Mod.Find<ModTile>("Smithas").Type;
		}
				public override void AddRecipes()
		{
			CreateRecipe()
.AddIngredient(3, 10)
.AddIngredient(ItemID.Sapphire)
.AddTile(TileID.WorkBenches)
.Register();
		}
	}
}
