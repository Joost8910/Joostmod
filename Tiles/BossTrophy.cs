using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace JoostMod.Tiles
{
	public class BossTrophy : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			DustType = 7;
			disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Trophy");
			AddMapEntry(new Color(120, 85, 60), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int item = 0;
			switch (frameX / 54)
			{
				case 0:
					item = Mod.Find<ModItem>("JumboCactuarTrophy").Type;
					break;
				case 1:
					item = Mod.Find<ModItem>("SAXTrophy").Type;
					break;
				case 2:
					item = Mod.Find<ModItem>("GilgameshTrophy").Type;
					break;
                case 3:
                    item = Mod.Find<ModItem>("GrandCactusWormTrophy").Type;
                    break;
            }
			if (item > 0)
			{
				Item.NewItem(i * 16, j * 16, 48, 48, item);
			}
		}
	}
}