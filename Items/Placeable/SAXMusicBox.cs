using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class SAXMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Vs. SA-X)");
			Tooltip.SetDefault("From Metroid Fusion");

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/VsSAX"), ModContent.ItemType<SAXMusicBox>(), ModContent.TileType<Tiles.SAXMusicBox>());
        }
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.SAXMusicBox>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
            Item.accessory = true;
		}
	}
}
