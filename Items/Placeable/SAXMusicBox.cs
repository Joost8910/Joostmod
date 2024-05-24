using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class SAXMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Vs. SA-X)");
            // Tooltip.SetDefault("From Metroid Fusion");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/VsSAX"), ModContent.ItemType<SAXMusicBox>(), ModContent.TileType<Tiles.SAXMusicBox>());
        }
		public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.SAXMusicBox>());
            Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
		}
	}
}
