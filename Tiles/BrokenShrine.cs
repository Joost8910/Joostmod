using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;

namespace JoostMod.Tiles
{
	public class BrokenShrine : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
            TileObjectData.addTile(Type);
            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
            TileID.Sets.DisableSmartCursor[Type] = true;/* tModPorter Note: Removed. Use TileID.Sets.TileID.Sets.DisableSmartCursor[Type] = true; instead */
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Shrine of Legends");
            AddMapEntry(new Color(0, 145, 100), name);
            DustType = 42;
            MineResist = 3f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
	}
}
