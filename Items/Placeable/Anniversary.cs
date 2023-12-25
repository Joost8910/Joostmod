using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class Anniversary : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Joostmod's Anniversary");
			Tooltip.SetDefault("'Celebrating one year of Cactus monsters and other stupid things'");
		}
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 34;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 500000;
			Item.rare = ItemRarityID.Yellow;
			Item.createTile = ModContent.TileType<Tiles.Anniversary>();
			Item.placeStyle = 0;
		}
	}
}