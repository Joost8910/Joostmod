using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class DeoremMuaMusicBox : ModItem
	{
		  public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Deorem/Mua Boss Fight)");
			Tooltip.SetDefault("From Metroid: Zero Mission");

            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DeoremMua"), ModContent.ItemType<DeoremMuaMusicBox>(), ModContent.TileType<Tiles.DeoremMuaMusicBox>());
        }
        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.DeoremMuaMusicBox>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Yellow;
			Item.value = 10000;
			Item.accessory = true;
		}
	}
}
