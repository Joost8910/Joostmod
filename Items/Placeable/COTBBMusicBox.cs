using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class COTBBMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Clash on the Big Bridge)");
			Tooltip.SetDefault("From Final Fantasy XII: The Zodiac Age");

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ClashOnTheBigBridge"), ModContent.ItemType<COTBBMusicBox>(), ModContent.TileType<Tiles.COTBBMusicBox>());
		}
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.COTBBMusicBox>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
            Item.accessory = true;
		}
	}
}
