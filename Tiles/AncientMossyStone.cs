using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Tiles
{
	public class AncientMossyStone : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //Main.tileMoss[Type] = true;
            //ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use Register//ItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Items.Legendaries.AncientStone>();
            AddMapEntry(new Color(18, 104, 60));
            DustType = 93;
            MinPick = 100;
            MineResist = 2f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            noItem = true;
            Main.tile[i, j].TileType = (ushort)ModContent.TileType<AncientStone>();
            WorldGen.SquareTileFrame(i, j, true);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
            }
            fail = true;

        }
        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

	}
}