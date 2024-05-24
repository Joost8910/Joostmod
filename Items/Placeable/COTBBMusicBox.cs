using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class COTBBMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Clash on the Big Bridge)");
            // Tooltip.SetDefault("From Final Fantasy XII: The Zodiac Age");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ClashOnTheBigBridge"), ModContent.ItemType<COTBBMusicBox>(), ModContent.TileType<Tiles.COTBBMusicBox>());
		}
		public override void SetDefaults()
		{
			Item.DefaultToMusicBox(ModContent.TileType<Tiles.COTBBMusicBox>());
			Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
		}
	}
}
