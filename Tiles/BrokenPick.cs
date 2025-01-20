using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using System.Collections.Generic;
using System.Collections;

namespace JoostMod.Tiles
{
	public class BrokenPick : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[] { 18 };
            TileObjectData.addTile(Type);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AddMapEntry(new Color(50, 50, 50));
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            int style = Main.tile[i, j].TileFrameX / 18;

            int itemType = ItemID.TinBar;
            int amount = 6;
            switch (style)
            {
                default:
                    itemType = ItemID.TinBar;
                    amount = Main.rand.Next(6, 8);
                    break;
                case 2:
                    itemType = ItemID.LeadBar;
                    amount = Main.rand.Next(7, 10);
                    break;
                case 3:
                    itemType = ItemID.TungstenBar;
                    amount = Main.rand.Next(7, 10);
                    break;
                case 4:
                    itemType = ItemID.PlatinumBar;
                    amount = Main.rand.Next(7, 10);
                    break;
                case 5:
                    itemType = ItemID.DemoniteBar;
                    amount = Main.rand.Next(8, 12);
                    break;
                case 0:
                    itemType = ItemID.CrimtaneBar;
                    amount = Main.rand.Next(8, 12);
                    break;
            }
            yield return new Item(itemType, amount);
        }
    }
}
