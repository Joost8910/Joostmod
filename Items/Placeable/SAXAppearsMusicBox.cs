using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class SAXAppearsMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Vs. SA-X)");
            // Tooltip.SetDefault("From Metroid Fusion");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SAXAppears"), ModContent.ItemType<SAXAppearsMusicBox>(), ModContent.TileType<Tiles.SAXAppearsMusicBox>());
        }
		public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.SAXAppearsMusicBox>());
            Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
		}
	}
}
