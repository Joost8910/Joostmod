using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace JoostMod.Tiles
{
	public class SeaStoneWest : ModTile
	{
		public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.AnchorTop = AnchorData.Empty;
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Stone of the West Sea");
			AddMapEntry(new Color(255, 255, 0), name);
			DustType = 103;
            TileID.Sets.DisableSmartCursor[Type] = true;/* tModPorter Note: Removed. Use TileID.Sets.TileID.Sets.DisableSmartCursor[Type] = true; instead */
        }

        public override bool RightClick(int i, int j)
        {
            WorldGen.KillTile(i, j, false, false, false);
            if (Main.netMode == 1 && !Main.tile[i, j].HasTile)
            {
                NetMessage.SendData(17, -1, -1, null, 4, (float)i, (float)j, 0f, 0, 0, 0);
            }
            return true;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Items.Legendaries.SeaStoneWest>();
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            drawData.glowTexture = drawData.drawTexture;
            drawData.glowSourceRect = new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight); ;
            drawData.glowColor = new Color(Main.DiscoB, Main.DiscoB, (255 - Main.DiscoB));
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = Main.DiscoB / 510f;
            g = Main.DiscoB / 510f;
            b = (255 - Main.DiscoB) / 510f;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Player player = Main.LocalPlayer;
                player.AddBuff(BuffID.Sonar, 30);
            }
        }
    }
}