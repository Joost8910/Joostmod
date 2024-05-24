using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class DeoremMuaMusicBox : ModItem
	{
		  public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (Deorem/Mua Boss Fight)");
            // Tooltip.SetDefault("From Metroid: Zero Mission");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DeoremMua"), ModContent.ItemType<DeoremMuaMusicBox>(), ModContent.TileType<Tiles.DeoremMuaMusicBox>());
        }
        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.DeoremMuaMusicBox>());
            Item.rare = ItemRarityID.Yellow;
			Item.value = 10000;
		}
	}
}
