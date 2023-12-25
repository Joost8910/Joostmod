using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class JumboCactuarTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jumbo Cactuar Trophy");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
			Item.createTile = ModContent.TileType<Tiles.BossTrophy>();
			Item.placeStyle = 0;
		}
	}
}