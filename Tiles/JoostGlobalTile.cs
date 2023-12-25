using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using JoostMod.Items.Weapons.Melee;
using JoostMod.Items.Weapons.Hybrid;

namespace JoostMod.Tiles
{
	internal sealed class JoostGlobalTile : GlobalTile
	{
		public override bool Drop(int i, int j, int type)
		{
			if (type == 186 && ((Main.tile[i, j].TileFrameX >= 828 && Main.tile[i, j].TileFrameX <= 844)||(Main.tile[i, j].TileFrameX >= 342 && Main.tile[i, j].TileFrameX <= 358)) && Main.tile[i, j].TileFrameY <= 16)//variant 15 for fake sword shrine, 6 for the sword in the skeleton, just the sword part so you dont get 6 at once
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<OldNail>());
			}
            if (type == TileID.Plants || type == TileID.Plants2 || type == TileID.JunglePlants || type == TileID.JunglePlants2)
            {
                if (WorldGen.genRand.NextBool(2) && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].HasItem(ModContent.ItemType<MightyBambooShoot>()))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ItemID.Seed);
                }
            }
            return true;
		}
	}
}
