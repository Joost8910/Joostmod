using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace JoostMod.Tiles
{
	public class JungleStone : ModTile
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
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Stone of Overgrowth");
			AddMapEntry(new Color(127, 255, 0), name);
			DustType = 39;
            disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
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
            player.cursorItemIconID = Mod.Find<ModItem>("JungleStone").Type;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            drawColor = new Color((int)(Main.DiscoG * 0.5f), 255, 0);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = (Main.DiscoG * 0.5f) / 510f;
            g = 0.5f;
            b = 0;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, Mod.Find<ModItem>("JungleStone").Type);
		}
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Player player = Main.LocalPlayer;
                player.AddBuff(BuffID.Swiftness, 30);
            }
        }
    }
}