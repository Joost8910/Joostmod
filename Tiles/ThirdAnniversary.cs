using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace JoostMod.Tiles
{
	public class ThirdAnniversary : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.AnchorTop = AnchorData.Empty;
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.addTile(Type);
			DustType = 7;
			disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Joostmod's Third Anniversary");
			AddMapEntry(new Color(81, 145, 86), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 48, Mod.Find<ModItem>("ThirdAnniversary").Type);
		}
	}
}