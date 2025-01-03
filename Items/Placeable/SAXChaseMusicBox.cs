using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class SAXChaseMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Vs. SA-X)");
            // Tooltip.SetDefault("From Metroid Fusion");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SAXChase"), ModContent.ItemType<SAXChaseMusicBox>(), ModContent.TileType<Tiles.SAXChaseMusicBox>());
        }
		public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.SAXChaseMusicBox>());
            Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
		}
	}
}
