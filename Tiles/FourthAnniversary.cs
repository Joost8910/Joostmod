using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace JoostMod.Tiles
{
	public class FourthAnniversary : ModTile
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
			TileID.Sets.DisableSmartCursor[Type] = true;/* tModPorter Note: Removed. Use TileID.Sets.TileID.Sets.DisableSmartCursor[Type] = true; instead */
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Joostmod's Fourth Anniversary");
			AddMapEntry(new Color(93, 137, 92), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, Mod.Find<ModItem>("FourthAnniversary").Type);
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
            zero.Y += 2;
            float alpha = 1000;
            if (Main.eclipse)
            {
                color = Lighting.GetColor(i, j);
                alpha = (float)(Main.time <= 200d ? (Main.time * 5d) : 1000d);
                if (alpha < 0)
                {
                    alpha = 0;
                }
                color.A = (byte)((int)(255f * (alpha / 1000f)));
                if (alpha > 0)
                {
                    Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Tiles/FourthAnniversarySolarEclipse").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }

            color = Lighting.GetColor(i, j, paintColor);
            alpha = (float)(Main.dayTime ? Main.time - 53000d : (32400d - Main.time));
            if (alpha < 0)
            {
                alpha = 0;
            }
            if (alpha > 1000)
            {
                alpha = 1000;
            }
            color.A = (byte)((int)(255f * (alpha / 1000f)));
            if (alpha > 0 && !(Main.bloodMoon && Main.time > 200 && Main.time < 31400) && !(Main.eclipse && Main.time > 200 && Main.time < 53000))
            {
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Tiles/FourthAnniversaryNight").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            color = Lighting.GetColor(i, j, paintColor);
            if (Main.bloodMoon)
            {
                color = Lighting.GetColor(i, j);
                alpha = (float)(Main.time <= 200d ? (Main.time * 5d) : (32400d - Main.time));
                if (alpha < 0)
                {
                    alpha = 0;
                }
                if (alpha > 1000)
                {
                    alpha = 1000;
                }
                color.A = (byte)((int)(255f * (alpha / 1000f)));
                if (alpha > 0)
                {
                    Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Tiles/FourthAnniversaryBloodMoon").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}