using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class SpongeStationHoney : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Absorbtion Pump");
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
			Item.createTile = Mod.Find<ModTile>("SpongeStationHoney").Type;
            Item.rare = ItemRarityID.Lime;
            Item.mech = true;
		}
		/*
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.HoneyAbsorbantSponge)
			.AddIngredient(ItemID.InletPump)
			.AddIngredient(ItemID.OutletPump)
			.AddIngredient(ItemID.Wire, 2)
			.AddTile(TileID.Anvils)
			.Register();
		}
		*/
	}
}