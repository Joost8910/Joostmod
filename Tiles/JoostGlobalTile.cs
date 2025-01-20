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
		public override void Drop(int i, int j, int type)/* tModPorter Suggestion: Use CanDrop to decide if items can drop, use this method to drop additional items. See documentation. */
		{
			if (type == TileID.LargePiles && ((Main.tile[i, j].TileFrameX >= 810 && Main.tile[i, j].TileFrameX < 846)||(Main.tile[i, j].TileFrameX >= 324 && Main.tile[i, j].TileFrameX < 360)))//variant 15 for fake sword shrine, 6 for the sword in the skeleton
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
		}
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (Main.tile[i, j-1].TileType == ModContent.TileType<ShrineOfLegends>() || Main.tile[i, j-1].TileType == ModContent.TileType<LegendGrave>())
            {
                if (Main.tile[i, j].TileType != ModContent.TileType<ShrineOfLegends>() && Main.tile[i, j].TileType != ModContent.TileType<LegendGrave>())
                {
                    return false;
                }
            }

            return base.CanKillTile(i, j, type, ref blockDamaged);
        }
    }
}
