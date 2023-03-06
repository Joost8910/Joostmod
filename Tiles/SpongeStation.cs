using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System;

namespace JoostMod.Tiles
{
	public class SpongeStation : ModTile
	{
		public override void SetStaticDefaults()
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
			name.SetDefault("Super Absorbtion Pump");
			AddMapEntry(new Color(105, 107, 125), name);
			DustType = 1;
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
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = Mod.Find<ModItem>("SpongeStation").Type;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, Mod.Find<ModItem>("SpongeStation").Type);
		}
        public override bool RightClick(int i, int j)
		{
			HitWire(i, j);
            return true;
        }
		public override void HitWire(int i, int j)
		{
			int x = i - (Main.tile[i, j].TileFrameX / 18) % 3;
            int y = j - (Main.tile[i, j].TileFrameY / 18) % 3;

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
                    if (Main.tile[l, m].LiquidAmount > 0)
                    {
                        SoundEngine.PlaySound(SoundID.SplashWeak, new Vector2(x * 16, y * 16));
                        Main.tile[l, m].LiquidAmount = 0;
                        Main.tile[l, m].lava/* tModPorter Suggestion: LiquidType = ... */(false);
                        Main.tile[l, m].honey/* tModPorter Suggestion: LiquidType = ... */(false);
                        WorldGen.SquareTileFrame(l, m, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.sendWater(l, m);
                        }
                        else
                        {
                            Liquid.AddWater(l, m);
                        }
                    }
                }
			}
		}
	}
}
