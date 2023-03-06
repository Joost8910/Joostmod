using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Tiles
{
	public class AncientStone : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            ItemDrop = Mod.Find<ModItem>("AncientStone").Type;
			AddMapEntry(new Color(56, 94, 51));
            DustType = 46;
            MinPick = 100;
            MineResist = 3f;
            HitSound = 21;
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