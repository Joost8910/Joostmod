using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class GraverobberMap : ModItem
	{
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
			Item.value = 0;
			Item.rare = ItemRarityID.Yellow;
			Item.createTile = ModContent.TileType<Tiles.GraverobberMap>();
			Item.placeStyle = 0;
		}
	}
}