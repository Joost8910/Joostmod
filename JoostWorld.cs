using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace JoostMod
{
    public class JoostWorld : ModSystem
    {
        private const int saveVersion = 0;
        public static bool downedJumboCactuar = false;
        public static bool downedSAX = false;
        public static bool downedGilgamesh = false;
        public static bool downedPinkzor = false;
        public static bool downedRogueTomato = false;
        public static bool downedWoodGuardian = false;
        public static bool downedFloweringCactoid = false;
        public static bool downedICU = false;

        public static bool downedSporeSpawn = false;
        public static bool downedRoc = false;
        public static bool downedSkeletonDemoman = false;
        public static bool downedCactusWorm = false;
        public static bool downedImpLord = false;
        public static bool downedStormWyvern = false;


        public static List<int> activeQuest = new List<int>();
        public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
        {
            downedJumboCactuar = false;
            downedSAX = false;
            downedGilgamesh = false;
            downedPinkzor = false;
            downedRogueTomato = false;
            downedWoodGuardian = false;
            downedFloweringCactoid = false;
            downedICU = false;

            downedSporeSpawn = false;
            downedRoc = false;
            downedSkeletonDemoman = false;
            downedCactusWorm = false;
            downedImpLord = false;
            downedStormWyvern = false;

            activeQuest = new List<int>();
        }
        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            if (downedJumboCactuar) tag["JumboCactuar"] = true;
            if (downedSAX) tag["SAX"] = true;
            if (downedGilgamesh) tag["Gilgamesh"] = true;
            if (downedPinkzor) tag["Pinkzor"] = true;
            if (downedRogueTomato) tag["RogueTomato"] = true;
            if (downedWoodGuardian) tag["WoodGuardian"] = true;
            if (downedFloweringCactoid) tag["FloweringCactoid"] = true;
            if (downedICU) tag["ICU"] = true;
            if (downedSporeSpawn) tag["SporeSpawn"] = true;
            if (downedRoc) tag["Roc"] = true;
            if (downedSkeletonDemoman) tag["SkeletonDemoMan"] = true;
            if (downedCactusWorm) tag["CactusWorm"] = true;
            if (downedImpLord) tag["ImpLord"] = true; ;
            if (downedStormWyvern) tag["StormWyvern"] = true;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            //var downed = tag.GetList<string>("downed");
            downedJumboCactuar = tag.ContainsKey("JumboCactuar");
            downedSAX = tag.ContainsKey("SAX");
            downedGilgamesh = tag.ContainsKey("Gilgamesh");
            downedPinkzor = tag.ContainsKey("Pinkzor");
            downedRogueTomato = tag.ContainsKey("RogueTomato");
            downedWoodGuardian = tag.ContainsKey("WoodGuardian");
            downedFloweringCactoid = tag.ContainsKey("FloweringCactoid");
            downedICU = tag.ContainsKey("ICU");
            downedSporeSpawn = tag.ContainsKey("SporeSpawn");
            downedRoc = tag.ContainsKey("Roc");
            downedSkeletonDemoman = tag.ContainsKey("SkeletonDemoMan");
            downedCactusWorm = tag.ContainsKey("CactusWorm");
            downedImpLord = tag.ContainsKey("ImpLord");
            downedStormWyvern = tag.ContainsKey("StormWyvern");
        }
        /*
        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedJumboCactuar = flags[0];
                downedSAX = flags[1];
                downedGilgamesh = flags[2];
                downedPinkzor = flags[3];
                downedRogueTomato = flags[4];
                downedWoodGuardian = flags[5];
                downedFloweringCactoid = flags[6];
                downedICU = flags[7];

                BitsByte flags2 = reader.ReadByte();
                downedSporeSpawn = flags2[0];
                downedRoc = flags2[1];
                downedSkeletonDemoman = flags2[2];
                downedCactusWorm = flags2[3];
                downedImpLord = flags2[4];
                downedStormWyvern = flags2[5];
            }
            else
            {
                ErrorLogger.Log("JoostMod: Unknown loadVersion: " + loadVersion);
            }
        }
        */

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedJumboCactuar;
            flags[1] = downedSAX;
            flags[2] = downedGilgamesh;
            flags[3] = downedPinkzor;
            flags[4] = downedRogueTomato;
            flags[5] = downedWoodGuardian;
            flags[6] = downedFloweringCactoid;
            flags[7] = downedICU;

            BitsByte flags2 = new BitsByte();
            flags2[0] = downedSporeSpawn;
            flags2[1] = downedRoc;
            flags2[2] = downedSkeletonDemoman;
            flags2[3] = downedCactusWorm;
            flags2[4] = downedImpLord;
            flags2[5] = downedStormWyvern;

            writer.Write(flags);
            writer.Write(flags2);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedJumboCactuar = flags[0];
            downedSAX = flags[1];
            downedGilgamesh = flags[2];
            downedPinkzor = flags[3];
            downedRogueTomato = flags[4];
            downedWoodGuardian = flags[5];
            downedFloweringCactoid = flags[6];
            downedICU = flags[7];

            BitsByte flags2 = reader.ReadByte();
            downedSporeSpawn = flags2[0];
            downedRoc = flags2[1];
            downedSkeletonDemoman = flags2[2];
            downedCactusWorm = flags2[3];
            downedImpLord = flags2[4];
            downedStormWyvern = flags2[5];
        }
        public override void PostWorldGen()
        {
            LegendShrine(Main.maxTilesX / 2, Main.maxTilesY / 2);
            bool flag = true;
            while (flag)
            {
                int i = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.6), (int)((double)Main.maxTilesX * 0.975));
                if (WorldGen.dungeonX > Main.maxTilesX / 2)
                {
                    i = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.025), (int)((double)Main.maxTilesX * 0.4));
                }
                int j = WorldGen.genRand.Next((int)Main.worldSurface + 20, (int)Main.rockLayer);
                if (Main.tile[i, j + 5].TileType == TileID.JungleGrass && !Main.tile[i, j + 4].HasTile)
                { 
                    flag = false;
                    StoneShrine(i, j, TileID.IridescentBrick, WallID.JungleUnsafe3, Mod.Find<ModTile>("JungleStone").Type, 4);
                }
            }
            bool flag2 = true;
            while (flag2)
            {
                int x = WorldGen.dungeonX + WorldGen.genRand.Next(200) - 100;
                int y = (int)Main.worldSurface + WorldGen.genRand.Next(400) + 10;
                if (!Main.tile[x - 1, y + 2].HasTile && Main.tile[x, y + 5].HasTile && Main.wallDungeon[Main.tile[x - 1, y + 2].WallType] && Main.tileDungeon[Main.tile[x, y + 5].TileType])
                {
                    flag2 = false;
                    int wallType = WallID.BlueDungeon;
                    if (Main.tile[x, y + 5].TileType == TileID.GreenDungeonBrick)
                    {
                        wallType = WallID.GreenDungeon;
                    }
                    if (Main.tile[x, y + 5].TileType == TileID.PinkDungeonBrick)
                    {
                        wallType = WallID.PinkDungeon;
                    }
                    StoneShrine(x, y, Main.tile[x, y + 5].TileType, wallType, Mod.Find<ModTile>("SkullStone").Type, 19);
                }
            }
            bool flag3 = true;
            while (flag3)
            {
                int a = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.1f), (int)(Main.maxTilesX * 0.9f));
                int b = Main.maxTilesY - 150 + WorldGen.genRand.Next(50);
                if (Main.tile[a, b + 5].HasTile && Main.tileSolid[Main.tile[a, b + 5].TileType] && !Main.tile[a, b + 4].HasTile)
                {
                    flag3 = false;
                    StoneShrine(a, b, TileID.HellstoneBrick, WallID.HellstoneBrickUnsafe, Mod.Find<ModTile>("InfernoStone").Type, 2);
                }
            }

            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers)
                {
                    // Frame 0 is Wooden Chests, Frame 1 is Gold Chest
                    int id = 0;
                    if (Main.tile[chest.x, chest.y].TileFrameX == 0 * 36)
                    {
                        int gen = WorldGen.genRand.Next(12);
                        switch (gen)
                        {
                            case 3:
                                id = Mod.Find<ModItem>("GlowingContacts").Type;
                                break;
                            case 6:
                                id = Mod.Find<ModItem>("ClawedGauntlet").Type;
                                break;
                            case 11:
                                id = Mod.Find<ModItem>("DirtBoard").Type;
                                break;
                            default:
                                break;
                        }
                    }
                    if (Main.tile[chest.x, chest.y].TileFrameX == 1 * 36)
                    {
                        int gen = WorldGen.genRand.Next(9);
                        switch (gen)
                        {
                            case 0:
                                id = Mod.Find<ModItem>("SapSpell").Type;
                                break;
                            case 1:
                                id = Mod.Find<ModItem>("DivineMirror").Type;
                                break;
                            case 4:
                                id = Mod.Find<ModItem>("ActualMace").Type;
                                break;
                            default:
                                break;
                        }
                    }
                    if (id > 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(id);
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void StoneShrine(int x, int y, int tile, int wall, int stone, byte color)
        {
            for (int i = x; i < x + 6; i++)
            {
                for (int j = y; j < y + 6; j++)
                {
                    Tile t = Main.tile[i, j];
                    t.HasTile = false;
                    t.LiquidAmount = 0;
                    t.Slope = 0;
                    t.IsHalfBlock = false;
                    WorldGen.KillWall(i, j);
                    WorldGen.PlaceWall(i, j, wall);
                    WorldGen.paintWall(i, j, color);
                }
                WorldGen.PlaceTile(i, y + 5, tile);
                WorldGen.paintTile(i, y + 5, color);
                WorldGen.PlaceTile(i, y, tile);
                WorldGen.paintTile(i, y, color);
            }
            WorldGen.PlaceTile(x, y + 1, tile);
            WorldGen.paintTile(x, y + 1, color);
            WorldGen.PlaceTile(x + 5, y + 1, tile);
            WorldGen.paintTile(x + 5, y + 1, color);
            WorldGen.PlaceObject(x + 3, y + 3, stone);
        }
        private void LegendShrine(int x, int y)
        {
            for (int i = x - 3; i <= x + 4; i++)
            {
                WorldGen.KillTile(i, y + 3);
                WorldGen.PlaceTile(i, y + 3, Mod.Find<ModTile>("AncientMossyStone").Type);
                //WorldGen.paintTile(i, y + 3, 18);

                WorldGen.KillTile(i, y - 3);
                WorldGen.PlaceTile(i, y - 3, Mod.Find<ModTile>("AncientMossyStone").Type);
                //WorldGen.paintTile(i, y - 3, 18);

                for (int j = y - 2; j <= y + 2; j++)
                {
                    Tile ti = Main.tile[i, j];
                    ti.HasTile = false;
                    ti.LiquidAmount = 0;
                    ti.Slope = 0;
                    ti.IsHalfBlock = false;
                    WorldGen.KillWall(i, j);
                    WorldGen.PlaceWall(i, j, 54);
                    WorldGen.paintWall(i, j, 18);
                }
            }
            WorldGen.KillTile(x - 4, y + 3);
            WorldGen.PlaceTile(x - 4, y + 3, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x - 4, y + 3, 18);

            WorldGen.KillTile(x + 5, y + 3);
            WorldGen.PlaceTile(x + 5, y + 3, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x + 5, y + 3, 18);

            WorldGen.KillTile(x - 3, y - 2);
            WorldGen.KillWall(x - 3, y - 2);
            WorldGen.PlaceTile(x - 3, y - 2, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x - 3, y - 2, 18);
            Tile t = Main.tile[x - 3, y - 2];
            t.Slope = SlopeType.SlopeUpLeft;

            WorldGen.KillTile(x + 4, y - 2);
            WorldGen.KillWall(x + 4, y - 2);
            WorldGen.PlaceTile(x + 4, y - 2, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x + 4, y - 2, 18);
            t = Main.tile[x + 4, y - 2];
            t.Slope = SlopeType.SlopeUpRight;

            WorldGen.KillTile(x - 4, y - 2);
            WorldGen.PlaceTile(x - 4, y - 2, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x - 4, y - 2, 18);
            t = Main.tile[x - 4, y - 2];
            t.Slope = SlopeType.SlopeDownRight;

            WorldGen.KillTile(x - 4, y - 1);
            WorldGen.PlaceTile(x - 4, y - 1, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x - 4, y - 1, 18);

            t= Main.tile[x - 3, y - 3];
            t.Slope = SlopeType.SlopeDownRight;

            WorldGen.KillTile(x + 5, y - 2);
            WorldGen.PlaceTile(x + 5, y - 2, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x + 5, y - 2, 18);
            t = Main.tile[x + 5, y - 2];
            t.Slope = SlopeType.SlopeDownLeft;

            WorldGen.KillTile(x + 5, y - 1);
            WorldGen.PlaceTile(x + 5, y - 1, Mod.Find<ModTile>("AncientMossyStone").Type);
            //WorldGen.paintTile(x + 5, y - 1, 18);

            t = Main.tile[x + 4, y - 3];
            t.Slope = SlopeType.SlopeDownLeft;

            WorldGen.PlaceObject(x, y + 2, Mod.Find<ModTile>("ShrineOfLegends").Type);
        }
    }
}