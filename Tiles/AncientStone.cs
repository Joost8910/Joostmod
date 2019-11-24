using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Tiles
{
	public class AncientStone : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            drop = mod.ItemType("AncientStone");
			AddMapEntry(new Color(56, 94, 51));
            dustType = 46;
            minPick = 100;
            mineResist = 3f;
            soundType = 21;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

	}
}