using Terraria.ID;
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
            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
            //ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use Register//ItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Items.Legendaries.AncientStone>();
            AddMapEntry(new Color(56, 94, 51));
            DustType = 46;
            MinPick = 100;
            MineResist = 3f;
            HitSound = SoundID.Tink;
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