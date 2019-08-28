using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Tiles
{
	internal sealed class JoostGlobalTile : GlobalTile
	{
		public override bool Drop(int i, int j, int type)
		{
			if (type == 186 && ((Main.tile[i, j].frameX >= 828 && Main.tile[i, j].frameX <= 844)||(Main.tile[i, j].frameX >= 342 && Main.tile[i, j].frameX <= 358)) && Main.tile[i, j].frameY <= 16)//variant 15 for fake sword shrine, 6 for the sword in the skeleton, just the sword part so you dont get 6 at once
			{
				Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("OldNail"));
			}
            if (type == TileID.Plants || type == TileID.Plants2 || type == TileID.JunglePlants || type == TileID.JunglePlants2)
            {
                if (WorldGen.genRand.Next(2) == 0 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].HasItem(mod.ItemType("MightyBambooShoot")))
                {
                    Item.NewItem(i * 16, j * 16, 32, 32, ItemID.Seed);
                }
            }
            return true;
		}
	}
}
