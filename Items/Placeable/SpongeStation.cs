using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class SpongeStation : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Absorbtion Pump");
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
			Item.createTile = Mod.Find<ModTile>("SpongeStation").Type;
            Item.rare = ItemRarityID.Blue;
            Item.mech = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
.AddIngredient(ItemID.SuperAbsorbantSponge)
.AddIngredient(ItemID.InletPump)
.AddIngredient(ItemID.OutletPump)
.AddIngredient(ItemID.Wire, 2)
.AddTile(TileID.Anvils)
.Register();
		}
	}
}