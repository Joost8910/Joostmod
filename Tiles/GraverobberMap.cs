using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using JoostMod.Items.Placeable;

namespace JoostMod.Tiles
{
	public class GraverobberMap : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.Width = 12;
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
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(199, 166, 127), name);
		}
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            Color paintColor = WorldGen.paintColor(Main.tile[i, j].TileColor);
            Color color = Lighting.GetColor(i, j, paintColor);

			Texture2D tex = ModContent.Request<Texture2D>($"{Texture}_X").Value;
			Vector2 startPos = new Vector2((i * 16) - ((Main.tile[i, j].TileFrameX / 18) * 16),
                (j * 16) - ((Main.tile[i, j].TileFrameY / 18) * 16));
			Rectangle rect = new Rectangle(0, 0, tex.Width, tex.Height);
			Vector2 origin = rect.Size() / 2;


            for (int k = 0; k < JoostWorld.graveLocations.Length; k++)
            {
                if (JoostWorld.graveLocations[k].value != Point16.Zero)
                {
                    //Main.NewText(k + ": " + JoostWorld.graveLocations[k].value, Color.White);

                    Vector2 loc = new Vector2(((float)JoostWorld.graveLocations[k].value.X / Main.maxTilesX) * (12 * 16 - 8) + 4,
                        ((float)JoostWorld.graveLocations[k].value.Y / Main.maxTilesY) * (4 * 16 - 12) + 6);
                    loc.X = (int)loc.X + (int)loc.X % 2; //Convert loc to an even integer to line up with map pixels
                    loc.Y = (int)loc.Y + (int)loc.Y % 2;

                    //Main.NewText(k + ": " + startPos, Color.Pink);
                    //Main.NewText(k + ": " + loc, Color.Purple);

                    Vector2 pos = startPos + loc;
                    bool prox = false;
                    for (int q = -1; q <= 1; q++) // Check if current drawn tile is within 1 tile of the X location
                    {
                        for (int r = -1; r <= 1; r++)
                        {
                            if (pos.ToTileCoordinates() == new Point(i + q, j + r))
                            {
                                prox = true;
                                break;
                            }
                        }
                    }
                    if (prox)
                    {
                        Main.spriteBatch.Draw(tex, pos - Main.screenPosition + zero, new Rectangle?(rect), color, 0, origin, 1f, SpriteEffects.None, 1);
                    }

                    //Main.NewText(k + ": " + pos, Color.Red);
                    //Main.NewText(k + ": " + pos.ToTileCoordinates(), Color.Yellow);
                    //Main.NewText(k + ": " + new Point(i, j), Color.Green);
                }
            }
        }
    }
}