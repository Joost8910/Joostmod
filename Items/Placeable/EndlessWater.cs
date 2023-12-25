using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class EndlessWater : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Water Pump");
            Tooltip.SetDefault("'Flood the world!'");
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 48;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.EndlessWater>();
            Item.rare = ItemRarityID.Blue;
            Item.mech = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
.AddIngredient(ItemID.BottomlessBucket)
.AddIngredient(ItemID.InletPump)
.AddIngredient(ItemID.OutletPump)
.AddIngredient(ItemID.Wire, 2)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}