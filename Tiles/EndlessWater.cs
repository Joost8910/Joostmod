using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System;

namespace JoostMod.Tiles
{
	public class EndlessWater : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileID.Sets.NotReallySolid[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3); 
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16 };
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Endless Water Pump");
			AddMapEntry(new Color(105, 107, 125), name);
			dustType = 1;
		}
		public override bool Slope(int i, int j)
		{
			return false;
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("EndlessWater");
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("EndlessWater"));
		}
public override void RightClick(int i, int j)
		{
			HitWire(i, j);
		}
		public override void HitWire(int i, int j)
		{
			int x = i - (Main.tile[i, j].frameX / 18) % 3;
            int y = j - (Main.tile[i, j].frameY / 18) % 3;

            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x, y + 2);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
            Wiring.SkipWire(x + 1, y + 2);
            Wiring.SkipWire(x + 2, y);
            Wiring.SkipWire(x + 2, y + 1);
            Wiring.SkipWire(x + 2, y + 2);
            for (int l = x; l < x + 3; l++)
			{
                for (int m = y; m < y + 3; m++)
                {
                    if (Main.tile[l, m].liquid == 0 || Main.tile[l,m].liquidType() == 0)
                    {
                        Main.PlaySound(19, x * 16, y * 16, 1);
                        Main.tile[l, m].liquidType(0);
                        Main.tile[l, m].liquid = 255;
                        WorldGen.SquareTileFrame(l, m, true);
                        if (Main.netMode == 1)
                        {
                            NetMessage.sendWater(l, m);
                        }
                    }
                }
			}
		}
	}
}
