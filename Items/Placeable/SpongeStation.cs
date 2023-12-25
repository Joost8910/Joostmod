using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class SpongeStation : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultra Absorbtion Pump");
			Tooltip.SetDefault("'Drain the world!'");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.height = 48;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.SpongeStation>();
			Item.rare = ItemRarityID.Yellow;
			Item.mech = true;
		}
		public override void AddRecipes()
        {
            /*
                CreateRecipe()
                .AddIngredient(ItemID.UltraAbsorbantSponge)
                .AddIngredient(ItemID.InletPump)
                .AddIngredient(ItemID.OutletPump)
                .AddIngredient(ItemID.Wire, 2)
                .AddTile(TileID.Anvils)
                .Register();
            */

            CreateRecipe()
            .AddIngredient<SpongeStationWater>()
            .AddIngredient(ItemID.LavaAbsorbantSponge)
            //.AddIngredient(ItemID.HoneyAbsorbantSponge)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
            CreateRecipe()
            .AddIngredient<SpongeStationLava>()
            .AddIngredient(ItemID.SuperAbsorbantSponge)
            //.AddIngredient(ItemID.HoneyAbsorbantSponge)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
            CreateRecipe()
            .AddIngredient<SpongeStationHoney>()
            .AddIngredient(ItemID.SuperAbsorbantSponge)
            .AddIngredient(ItemID.LavaAbsorbantSponge)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}