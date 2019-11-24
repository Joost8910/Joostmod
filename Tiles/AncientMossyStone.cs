using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Tiles
{
	public class AncientMossyStone : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileMoss[Type] = true;
            drop = mod.ItemType("AncientStone");
            AddMapEntry(new Color(18, 104, 60));
            dustType = 93;
            minPick = 100;
            mineResist = 2f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)
            {
                noItem = true;
                Main.tile[i, j].type = (ushort)mod.TileType("AncientStone");
                WorldGen.SquareTileFrame(i, j, true);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
                }
            }

        }
        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

	}
}