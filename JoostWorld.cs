using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using JoostMod.Items.Accessories;
using JoostMod.Items.Mounts;
using JoostMod.Items.Tools;
using JoostMod.Items.Weapons.Melee;
using JoostMod.Items.Weapons.Magic;
using Terraria.WorldBuilding;
using JoostMod.Tiles;
using JoostMod.Items.Consumables;
using Microsoft.VisualBasic;
using Terraria.DataStructures;
using System.Linq;
using ReLogic.Utilities;
using System;
using Terraria.IO;
using Terraria.Localization;
using static System.Net.WebRequestMethods;

namespace JoostMod
{
    public class JoostWorld : ModSystem
    {
        private const int saveVersion = 0;
        public static bool downedJumboCactuar = false;
        public static bool downedSAX = false;
        public static bool downedGilgamesh = false;

        public static bool downedEaterofWorlds = false;
        public static bool downedBrainofCthulhu = false;

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

        public static PosData<Point16>[] graveLocations = new PosData<Point16>[6]; 
        public static LocalizedText LegendShrineGenPassMessage {  get; private set; }
        public static LocalizedText LegendGravesGenPassMessage { get; private set; }

        public static List<int> activeQuest = new List<int>();

        public override void SetStaticDefaults()
        {
            LegendShrineGenPassMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(LegendShrineGenPassMessage)}"));

            LegendGravesGenPassMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(LegendGravesGenPassMessage)}"));
        }

        public override void ClearWorld()
        {
            downedJumboCactuar = false;
            downedSAX = false;
            downedGilgamesh = false;

            downedEaterofWorlds = false;
            downedBrainofCthulhu = false;

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
            graveLocations = new PosData<Point16>[6];
        }
        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            if (downedJumboCactuar) tag["JumboCactuar"] = true;
            if (downedSAX) tag["SAX"] = true;
            if (downedGilgamesh) tag["Gilgamesh"] = true;

            if (downedEaterofWorlds) tag["EaterofWorlds"] = true;
            if (downedBrainofCthulhu) tag["BrainofCthulhu"] = true;

            if (downedPinkzor) tag["Pinkzor"] = true;
            if (downedRogueTomato) tag["RogueTomato"] = true;
            if (downedWoodGuardian) tag["WoodGuardian"] = true;
            if (downedFloweringCactoid) tag["FloweringCactoid"] = true;
            if (downedICU) tag["ICU"] = true;
            if (downedSporeSpawn) tag["SporeSpawn"] = true;
            if (downedRoc) tag["Roc"] = true;
            if (downedSkeletonDemoman) tag["SkeletonDemoMan"] = true;
            if (downedCactusWorm) tag["CactusWorm"] = true;
            if (downedImpLord) tag["ImpLord"] = true;

            if (downedStormWyvern) tag["StormWyvern"] = true;

            if (graveLocations.Length > 0)
            {
                tag["GraveLocations"] = graveLocations.Select(info => new TagCompound
                {
                    ["pos"] = info.pos,
                    ["value"] = info.value
                }).ToList();
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            //var downed = tag.GetList<string>("downed");
            downedJumboCactuar = tag.ContainsKey("JumboCactuar");
            downedSAX = tag.ContainsKey("SAX");
            downedGilgamesh = tag.ContainsKey("Gilgamesh");

            downedEaterofWorlds = tag.ContainsKey("EaterofWorlds");
            downedBrainofCthulhu = tag.ContainsKey("BrainofCthulhu");

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


            List<PosData<Point16>> list = new List<PosData<Point16>>(6);
            foreach (var entry in tag.GetList<TagCompound>("GraveLocations"))
            {
                list.Add(new PosData<Point16>(
                    entry.GetInt("pos"),
                    entry.Get<Point16>("value")
                ));
            }
            graveLocations = list.ToArray();
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
            flags2[6] = downedEaterofWorlds;
            flags2[7] = downedBrainofCthulhu;

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
            downedEaterofWorlds = flags2[6];
            downedBrainofCthulhu = flags2[7];
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int buriedChestIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Buried Chests"));
            if (buriedChestIndex != -1)
            {
                tasks.Insert(buriedChestIndex - 1, new LegendShrinesGenPass("Placing Shrines of Legend", 100f));
            }
            if (Main.remixWorld)
            {
                int finalIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
                if (finalIndex != -1)
                {
                    tasks.Insert(finalIndex + 1, new LegendGravesGenPass("Burying Legends", 100f));
                }
            }
            else
            {
                int JungleWallIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Muds Walls In Jungle"));
                if (JungleWallIndex != -1)
                {
                    tasks.Insert(JungleWallIndex + 1, new LegendShrinesGenPass("Placing Shrine of Overgrowth", 100f));
                }
            }
        }
        public override void PostWorldGen()
        {
            if (Main.remixWorld)
            {
            }
            else
            {
            }

            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null)
                {
                    bool setOne = Main.tile[chest.x, chest.y].TileType == TileID.Containers;
                    bool setTwo = Main.tile[chest.x, chest.y].TileType == TileID.Containers2;
                    // Frame 0 is Wooden Chests, Frame 1 is Gold Chest, 8 is Rich Mahogany, 17 is Water, 32 is Mushroom, 50 is Granite, 51 is Marble
                    // Frame 4 of Containers 2 is Dead Man's Chest, 2 is Sandstone Chest
                    int frame = Main.tile[chest.x, chest.y].TileFrameX;
                    int id = 0;
                    int id2 = 0;
                    if (setOne && frame == 0 * 36)
                    {
                        int gen = WorldGen.genRand.Next(10);
                        switch (gen)
                        {
                            case 3:
                                id = ModContent.ItemType<GlowingContacts>();
                                break;
                            case 4:
                                id = ModContent.ItemType<ClawedGauntlet>();
                                break;
                            case 5:
                                id = ModContent.ItemType<VaultingPole>();
                                break;
                            case 9:
                                id = ModContent.ItemType<DirtBoardItem>();
                                break;
                            default:
                                break;
                        }
                        if (WorldGen.genRand.NextBool(3))
                        {
                            id2 = ModContent.ItemType<ClearStar>();
                        }
                    }
                    if ((setOne && (frame == 1 * 36 || frame == 8 * 36 || frame == 32 * 36 || frame == 50 * 36 || frame == 51 * 36)) || (setTwo && frame == 4 * 36))
                    {
                        int gen = WorldGen.genRand.Next(8);
                        switch (gen)
                        {
                            case 0:
                                id = ModContent.ItemType<SapSpell>();
                                break;
                            case 1:
                                id = ModContent.ItemType<DivineMirror>();
                                break;
                            case 4:
                                id = ModContent.ItemType<ActualMace>();
                                break;
                            case 5:
                                id = ModContent.ItemType<ClawedGauntlet>();
                                break;
                            default:
                                break;
                        }
                        if (WorldGen.genRand.NextBool(5))
                        {
                            id2 = ModContent.ItemType<SlimeStar>();
                        }
                    }
                    if (setOne && frame == 17 * 36)
                    {
                        if (WorldGen.genRand.NextBool(3))
                        {
                            id2 = ModContent.ItemType<RainStar>();
                        }
                    }
                    if (setTwo && frame == 10 * 36)
                    {
                        if (WorldGen.genRand.NextBool(20))
                        {
                            id = ModContent.ItemType<CactusBait>();
                        }
                        if (WorldGen.genRand.NextBool(3))
                        {
                            id2 = ModContent.ItemType<SandstormStar>();
                        }
                    }
                    if (id > 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == ItemID.None)
                            {
                                chest.item[inventoryIndex].SetDefaults(id);
                                break;
                            }
                        }
                    }
                    if (id2 > 0) //Repasting this is ugly but im too lazy to write something clean rn
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == ItemID.None)
                            {
                                chest.item[inventoryIndex].SetDefaults(id2);
                                break;
                            }
                        }
                    }

                }
            }
        }
        public static void StoneShrine(int x, int y, int tile, int wall, int stone, byte color)
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
            WorldGen.PlaceObject(x + 2, y + 3, stone);
        }
        public static void LegendShrine(int x, int y, bool isBroken = false)
        {
            int stoneType = ModContent.TileType<AncientMossyStone>();
            for (int i = x - 3; i <= x + 4; i++)
            {
                WorldGen.KillTile(i, y + 3);
                WorldGen.PlaceTile(i, y + 3, stoneType, true, true);

                WorldGen.KillTile(i, y - 3);
                WorldGen.PlaceTile(i, y - 3, stoneType, true, true);
                if (isBroken)
                { 
                    WorldGen.paintTile(i, y + 3, PaintID.BrownPaint);
                    WorldGen.paintTile(i, y - 3, PaintID.BrownPaint);
                }

                for (int j = y - 2; j <= y + 2; j++)
                {
                    Tile ti = Main.tile[i, j];
                    ti.HasTile = false;
                    ti.LiquidAmount = 0;
                    ti.LiquidType = 0;
                    ti.Slope = 0;
                    ti.IsHalfBlock = false;
                    WorldGen.KillWall(i, j);
                    WorldGen.PlaceWall(i, j, 54);

                    WorldGen.paintWall(i, j, isBroken ? PaintID.OrangePaint : PaintID.DeepTealPaint);
                }
            }
            WorldGen.KillTile(x - 4, y + 3);
            WorldGen.PlaceTile(x - 4, y + 3, stoneType, true, true);

            WorldGen.KillTile(x + 5, y + 3);
            WorldGen.PlaceTile(x + 5, y + 3, stoneType, true, true);

            WorldGen.KillTile(x - 3, y - 2);
            WorldGen.KillWall(x - 3, y - 2);
            WorldGen.PlaceTile(x - 3, y - 2, stoneType, true, true);
            Tile t = Main.tile[x - 3, y - 2];
            t.Slope = SlopeType.SlopeUpLeft;

            WorldGen.KillTile(x + 4, y - 2);
            WorldGen.KillWall(x + 4, y - 2);
            WorldGen.PlaceTile(x + 4, y - 2, stoneType, true, true);
            t = Main.tile[x + 4, y - 2];
            t.Slope = SlopeType.SlopeUpRight;

            WorldGen.KillTile(x - 4, y - 2);
            WorldGen.PlaceTile(x - 4, y - 2, stoneType, true, true);
            t = Main.tile[x - 4, y - 2];
            if (!Main.tile[x - 4, y - 3].HasTile)
                t.Slope = SlopeType.SlopeDownRight;

            WorldGen.KillTile(x - 4, y - 1);
            WorldGen.PlaceTile(x - 4, y - 1, stoneType, true, true);

            t= Main.tile[x - 3, y - 3];
            if (!Main.tile[x - 3, y - 4].HasTile)
                t.Slope = SlopeType.SlopeDownRight;

            WorldGen.KillTile(x + 5, y - 2);
            WorldGen.PlaceTile(x + 5, y - 2, stoneType, true, true);
            t = Main.tile[x + 5, y - 2];
            if (!Main.tile[x + 5, y - 3].HasTile)
                t.Slope = SlopeType.SlopeDownLeft;

            WorldGen.KillTile(x + 5, y - 1);
            WorldGen.PlaceTile(x + 5, y - 1, stoneType, true, true);

            t = Main.tile[x + 4, y - 3];
            if (!Main.tile[x + 4, y - 4].HasTile)
                t.Slope = SlopeType.SlopeDownLeft;

            if (isBroken)
            {
                WorldGen.paintTile(x - 4, y + 3, PaintID.BrownPaint);
                WorldGen.paintTile(x + 5, y + 3, PaintID.BrownPaint);
                WorldGen.paintTile(x - 3, y - 2, PaintID.BrownPaint);
                WorldGen.paintTile(x + 4, y - 2, PaintID.BrownPaint);
                WorldGen.paintTile(x - 4, y - 2, PaintID.BrownPaint);
                WorldGen.paintTile(x - 4, y - 1, PaintID.BrownPaint);
                WorldGen.paintTile(x + 5, y - 2, PaintID.BrownPaint);
                WorldGen.paintTile(x + 5, y - 1, PaintID.BrownPaint);
            }

            int type = isBroken ? ModContent.TileType<BrokenShrine>() : ModContent.TileType<ShrineOfLegends>();

            WorldGen.PlaceObject(x, y + 2, type);
        }

        public static void GraverobberOutpost(int x, int y)
        {
            Rectangle current = new Rectangle(x, y, 16, 10);
            for (int i = x; i < x + current.Width; i++)
            {
                for (int j = y; j < y + current.Height; j++)
                {
                    Tile tile = Main.tile[i, j];
                    tile.LiquidAmount = 0;
                    tile.LiquidType = 0;
                }
            }
            ushort tileType = TileID.WoodBlock;
            ushort wallType = WallID.Planked;
            WorldUtils.Gen(new Point(current.X, current.Y), new Shapes.Rectangle(current.Width, current.Height), Actions.Chain(new GenAction[]
            {
                new Actions.SetTileKeepWall(tileType, false, true),
                new Actions.SetFrames(true)
            }));
            WorldUtils.Gen(new Point(current.X + 1, current.Y + 1), new Shapes.Rectangle(current.Width - 2, current.Height - 2), Actions.Chain(new GenAction[]
            {
                new Actions.ClearTile(true),
                new Actions.PlaceWall(wallType, true)
            }));

            int doorY = current.Y + current.Height - 4;
            WorldUtils.Gen(new Point(current.X - 2, doorY), new Shapes.Rectangle(3, 3), new Actions.ClearTile(true));
            WorldGen.PlaceTile(current.X, doorY, TileID.ClosedDoor, true, true, -1, 0);

            WorldUtils.Gen(new Point(current.X + 15, doorY), new Shapes.Rectangle(1, 3), new Actions.ClearTile(true));

            for (int i = 0; i < 20; i++)
            {
                Point p = new Point(current.X + 16 + i, doorY);
                if (Main.tile[p] != null)
                {
                    WorldUtils.Gen(p, new Shapes.Rectangle(1, 3), new Actions.ClearTile(true));
                }
                else
                {
                    break;
                }
            }
            WorldUtils.Gen(new Point(current.X + 15, doorY), new Shapes.Rectangle(13, 3), Actions.Chain(new GenAction[]
            {
                new Actions.ClearTile(true)
            }));

            WorldGen.PlaceTile(current.X + 15, doorY, TileID.ClosedDoor, true, true, -1, 0);
            WorldGen.PlaceTile(current.X + current.Width, current.Y + current.Height - 1, TileID.Platforms, true);
            WorldGen.PlaceTile(current.X - 1, current.Y + current.Height - 1, TileID.Platforms, true);

            WorldGen.PlaceObject(current.X + 5, current.Y + 4, ModContent.TileType<GraverobberMap>(), true);

            WorldGen.PlaceObject(current.X + 3, current.Y + current.Height - 2, TileID.Anvils, true);
            WorldGen.PlaceObject(current.X + 6, current.Y + current.Height - 2, TileID.Furnaces, true);
            WorldGen.PlaceObject(current.X + 8, current.Y + current.Height - 2, TileID.Chairs, true, 0, 0, -1, 1);
            WorldGen.PlaceObject(current.X + 9, current.Y + current.Height - 2, TileID.WorkBenches, true);
            WorldGen.AddBuriedChest(new Point(current.X + 12, current.Y + current.Height - 1));
        }

        public static void LegendGrave(int x, int y, int graveType)
        {
            WorldUtils.Gen(new Point(x + 1, y), new Shapes.Rectangle(4, 1), Actions.Chain(new GenAction[]
            {
                new Actions.ClearTile(true)
            }));
            WorldUtils.Gen(new Point(x, y + 1), new Shapes.Rectangle(6, 4), Actions.Chain(new GenAction[]
            {
                new Actions.ClearTile(true)
            }));

            ushort type = (ushort)ModContent.TileType<AncientStone>();
            WorldUtils.Gen(new Point(x + 1, y + 3), new Shapes.Rectangle(4, 1), Actions.Chain(new GenAction[]
            {
                new Actions.PlaceTile(type),
                new Actions.SetFrames(true)
            }));
            WorldUtils.Gen(new Point(x, y + 4), new Shapes.Rectangle(6, 1), Actions.Chain(new GenAction[]
            {
                new Actions.PlaceTile(type),
                new Actions.SetFrames(true)
            }));

            WorldGen.PlaceObject(x + 2, y + 2, ModContent.TileType<LegendGrave>(), true, graveType);
            if (graveType == 0) //David's Gnomes
            {
                WorldGen.KillTile(x -1, y + 4);
                WorldGen.PlaceObject(x - 1, y + 4, TileID.GardenGnome, true, 0, 0, 1, 1);
                WorldGen.PlaceObject(x, y + 3, TileID.GardenGnome, true, 0, 0, -1, 1);
                WorldGen.PlaceObject(x + 5, y + 3, TileID.GardenGnome, true);
                WorldGen.KillTile(x + 6, y + 4);
                WorldGen.PlaceObject(x + 6, y + 4, TileID.GardenGnome, true);
            }
            WorldGen.PlaceObject(x + 1, y + 2, ModContent.TileType<BrokenPick>(), true, graveType);
            graveLocations[graveType] = new PosData<Point16>(graveType, new Point16(x + 2, y + 2));
        }

        public static void UncleCariusCave(int i, int j, ushort sandID)
        {
            if (GenVars.numOceanCaveTreasure >= GenVars.maxOceanCaveTreasure)
            {
                GenVars.numOceanCaveTreasure = 0;
            }
            Vector2D vector2D = default(Vector2D);
            vector2D.X = (double)i;
            vector2D.Y = (double)j;
            Vector2D vector2D2 = default(Vector2D);
            if (i < Main.maxTilesX / 2)
            {
                vector2D2.X = 0.25 + WorldGen.genRand.NextDouble() * 0.25;
            }
            else
            {
                vector2D2.X = -0.35 - WorldGen.genRand.NextDouble() * 0.5;
            }
            vector2D2.Y = 0.4 + WorldGen.genRand.NextDouble() * 0.25;
            ushort num = 264;
            ushort sandType = sandID;
            ushort hardSandType = TileID.HardenedSand;
            if (sandID != TileID.Sand)
            {
                hardSandType = sandID == TileID.Ebonsand ? TileID.CorruptHardenedSand : TileID.CrimsonHardenedSand;
            }
            double num4 = (double)WorldGen.genRand.Next(17, 25);
            double num5 = (double)WorldGen.genRand.Next(500, 700);
            double num6 = 4.0;
            bool flag = true;
            while (num4 > num6 && num5 > 0.0)
            {
                bool flag2 = true;
                bool flag3 = true;
                bool flag4 = true;
                if (vector2D.X > (double)(WorldGen.beachDistance - 50) && vector2D.X < (double)(Main.maxTilesX - WorldGen.beachDistance + 50))
                {
                    num4 *= 0.96;
                    num5 *= 0.96;
                }

                if (num4 < num6 + 2.0 || num5 < 20.0)
                {
                    flag4 = false;
                }
                if (flag)
                {
                    num4 -= 0.01 + WorldGen.genRand.NextDouble() * 0.01;
                    num5 -= 0.5;
                }
                else
                {
                    num4 -= 0.02 + WorldGen.genRand.NextDouble() * 0.02;
                    num5 -= 1.0;
                }
                int num7 = (int)(vector2D.X - num4 * 3.0);
                int num8 = (int)(vector2D.X + num4 * 3.0);
                int num9 = (int)(vector2D.Y - num4 * 3.0);
                int num10 = (int)(vector2D.Y + num4 * 3.0);
                if (num7 < 1)
                {
                    num7 = 1;
                }
                if (num8 > Main.maxTilesX - 1)
                {
                    num8 = Main.maxTilesX - 1;
                }
                if (num9 < 1)
                {
                    num9 = 1;
                }
                if (num10 > Main.maxTilesY - 1)
                {
                    num10 = Main.maxTilesY - 1;
                }
                for (int k = num7; k < num8; k++)
                {
                    for (int l = num9; l < num10; l++)
                    {
                        if (!BadOceanCaveTiles(k, l))
                        {
                            double num11 = new Vector2D(Math.Abs((double)k - vector2D.X), Math.Abs((double)l - vector2D.Y)).Length();
                            if (flag4 && num11 < num4 * 0.5 + 1.0)
                            {
                                Main.tile[k, l].TileType = num;
                                Main.tile[k, l].ClearTile();
                            }
                            else if (num11 < num4 * 1.5 + 1.0 && Main.tile[k, l].TileType != num)
                            {

                                if ((double)l < vector2D.Y)
                                {
                                    if ((vector2D2.X < 0.0 && (double)k < vector2D.X) || (vector2D2.X > 0.0 && (double)k > vector2D.X))
                                    {
                                        if (num11 < num4 * 1.1 + 1.0)
                                        {
                                            Main.tile[k, l].TileType = hardSandType;
                                            if (Main.tile[k, l].LiquidAmount == 255)
                                            {
                                                Main.tile[k, l].WallType = 0;
                                            }
                                        }
                                        else if (Main.tile[k, l].TileType != hardSandType)
                                        {
                                            Main.tile[k, l].TileType = sandType;
                                        }
                                    }
                                }
                                else if ((vector2D2.X < 0.0 && k < i) || (vector2D2.X > 0.0 && k > i))
                                {
                                    if (Main.tile[k, l].LiquidAmount == 255)
                                    {
                                        Main.tile[k, l].TileType = 0;
                                    }
                                    WorldGen.PlaceTile(k, l, sandType, true, true);
                                    //Main.tile[k, l].TileType = num2;
                                    //Main.tile[k, l].active(true);
                                    if (k == (int)vector2D.X & flag2)
                                    {
                                        flag2 = false;
                                        int num12 = 30 + WorldGen.genRand.Next(3);
                                        int num13 = 23 + WorldGen.genRand.Next(3);
                                        int num14 = 10 + WorldGen.genRand.Next(3);
                                        int num15 = k;
                                        int num16 = k + num14;
                                        if (vector2D2.X < 0.0)
                                        {
                                            num15 = k - num14;
                                            num16 = k;
                                        }
                                        if (num5 < 100.0)
                                        {
                                            num12 = (int)((double)num12 * (num5 / 100.0));
                                            num13 = (int)((double)num13 * (num5 / 100.0));
                                            num14 = (int)((double)num14 * (num5 / 100.0));
                                        }
                                        if (num4 < num6 + 5.0)
                                        {
                                            double num17 = (num4 - num6) / 5.0;
                                            num12 = (int)((double)num12 * num17);
                                            num13 = (int)((double)num13 * num17);
                                            num14 = (int)((double)num14 * num17);
                                        }
                                        for (int m = num15; m <= num16; m++)
                                        {
                                            int num18 = l;
                                            while (num18 < l + num12 && !BadOceanCaveTiles(m, num18))
                                            {
                                                if (num18 > l + num13)
                                                {
                                                    if (WorldGen.SolidTile(m, num18, false) && Main.tile[m, num18].TileType != sandType)
                                                    {
                                                        break;
                                                    }
                                                    //Main.tile[m, num18].TileType = num3;
                                                    WorldGen.PlaceTile(m, num18, hardSandType, true, true);
                                                }
                                                else
                                                {
                                                    //Main.tile[m, num18].TileType = num2;
                                                    WorldGen.PlaceTile(m, num18, sandType, true, true);
                                                }
                                                //Main.tile[m, num18].active(true);
                                                if (WorldGen.genRand.Next(3) == 0)
                                                {
                                                    //*Main.tile[m - 1, num18].type = num2;
                                                    //Main.tile[m - 1, num18].active(true);
                                                    WorldGen.PlaceTile(m - 1, num18, hardSandType, true, true);
                                                }
                                                if (WorldGen.genRand.Next(3) == 0)
                                                {
                                                    //*Main.tile[m + 1, num18].type = num2;
                                                    //Main.tile[m + 1, num18].active(true);
                                                    WorldGen.PlaceTile(m + 1, num18, hardSandType, true, true);
                                                }
                                                num18++;
                                            }
                                        }
                                    }
                                }
                            }
                            if (num11 < num4 * 1.3 + 1.0 && l > j - 10)
                            {
                                Main.tile[k, l].LiquidAmount = 255;
                            }
                            if (flag3 && k == (int)vector2D.X && (double)l > vector2D.Y)
                            {
                                flag3 = false;
                                int num19 = 100;
                                int num20 = 2;
                                for (int n = k - num20; n <= k + num20; n++)
                                {
                                    for (int num21 = l; num21 < l + num19; num21++)
                                    {
                                        if (!BadOceanCaveTiles(n, num21))
                                        {
                                            Main.tile[n, num21].LiquidAmount = 255;
                                        }
                                        else
                                        {
                                            num4 *= 0.99;
                                            num5 *= 0.98;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                vector2D += vector2D2;
                vector2D2.X += WorldGen.genRand.NextDouble() * 0.1 - 0.05;
                vector2D2.Y += WorldGen.genRand.NextDouble() * 0.1 - 0.05;
                if (flag)
                {
                    if (vector2D.Y > (Main.worldSurface * 2.0 + Main.rockLayer) / 3.0 && vector2D.Y > (double)(j + 30))
                    {
                        flag = false;
                    }
                    vector2D2.Y = Utils.Clamp<double>(vector2D2.Y, 0.35, 1.0);
                }
                else
                {
                    if (vector2D.X < (double)(Main.maxTilesX / 2))
                    {
                        if (vector2D2.X < 0.5)
                        {
                            vector2D2.X += 0.02;
                        }
                    }
                    else if (vector2D2.X > -0.5)
                    {
                        vector2D2.X -= 0.02;
                    }
                    if (!flag4)
                    {
                        if (vector2D2.Y < 0)
                        {
                            vector2D2.Y *= 0.95;
                        }
                        vector2D2.Y += 0.04;
                    }
                    else if (vector2D.Y < (Main.worldSurface * 4.0 + Main.rockLayer) / 5.0)
                    {
                        if (vector2D2.Y < 0.0)
                        {
                            vector2D2.Y *= 0.97;
                        }
                        vector2D2.Y += 0.02;
                    }
                    else if (vector2D2.Y > -0.1)
                    {
                        vector2D2.Y *= 0.99;
                        vector2D2.Y -= 0.01;
                    }
                    vector2D2.Y = Utils.Clamp<double>(vector2D2.Y, -1.0, 1.0);
                }
                if (vector2D.X < (double)(Main.maxTilesX / 2))
                {
                    vector2D2.X = Utils.Clamp<double>(vector2D2.X, 0.1, 1.0);
                }
                else
                {
                    vector2D2.X = Utils.Clamp<double>(vector2D2.X, -1.0, -0.1);
                }

            }

            vector2D -= vector2D2 * 6;
            bool check = true;
            bool check2 = true;
            while (check || check2)
            {
                //WorldGen.digTunnel(vector2D.X, vector2D.Y, vector2D2.X, vector2D2.Y, 1, (int)num4 * 3);
                bool flag2 = true;
                bool flag3 = true;
                int num7 = (int)(vector2D.X - num4 * 3.0);
                int num8 = (int)(vector2D.X + num4 * 3.0);
                int num9 = (int)(vector2D.Y - num4 * 3.0);
                int num10 = (int)(vector2D.Y + num4 * 3.0);
                if (num7 < 1)
                {
                    num7 = 1;
                }
                if (num8 > Main.maxTilesX - 1)
                {
                    num8 = Main.maxTilesX - 1;
                }
                if (num9 < 1)
                {
                    num9 = 1;
                }
                if (num10 > Main.maxTilesY - 1)
                {
                    num10 = Main.maxTilesY - 1;
                }
                for (int k = num7; k < num8; k++)
                {
                    for (int l = num9; l < num10; l++)
                    {
                        if (!BadOceanCaveTiles(k, l))
                        {
                            double num11 = new Vector2D(Math.Abs((double)k - vector2D.X), Math.Abs((double)l - vector2D.Y)).Length();
                            if (num11 < num4 * 0.5 + 1.0)
                            {
                                Main.tile[k, l].TileType = num;
                                Main.tile[k, l].ClearTile();
                            }
                            else if (num11 < num4 * 1.5 + 1.0 && Main.tile[k, l].TileType != num)
                            {

                                if ((double)l < vector2D.Y)
                                {
                                    if ((vector2D2.X < 0.0 && (double)k < vector2D.X) || (vector2D2.X > 0.0 && (double)k > vector2D.X))
                                    {
                                        if (num11 < num4 * 1.1 + 1.0)
                                        {
                                            Main.tile[k, l].TileType = hardSandType;
                                            if (Main.tile[k, l].LiquidAmount == 255)
                                            {
                                                Main.tile[k, l].WallType = 0;
                                            }
                                        }
                                        else if (Main.tile[k, l].TileType != hardSandType)
                                        {
                                            Main.tile[k, l].TileType = hardSandType;
                                        }
                                    }
                                }
                                else if ((vector2D2.X < 0.0 && k < i) || (vector2D2.X > 0.0 && k > i))
                                {
                                    if (Main.tile[k, l].LiquidAmount == 255)
                                    {
                                        Main.tile[k, l].TileType = 0;
                                    }
                                    WorldGen.PlaceTile(k, l, hardSandType, true, true);
                                    //Main.tile[k, l].TileType = num2;
                                    //Main.tile[k, l].active(true);
                                    if (k == (int)vector2D.X & flag2)
                                    {
                                        flag2 = false;
                                        int num12 = 50 + WorldGen.genRand.Next(3);
                                        int num13 = 43 + WorldGen.genRand.Next(3);
                                        int num14 = 20 + WorldGen.genRand.Next(3);
                                        int num15 = k;
                                        int num16 = k + num14;
                                        if (vector2D2.X < 0.0)
                                        {
                                            num15 = k - num14;
                                            num16 = k;
                                        }
                                        if (num5 < 100.0)
                                        {
                                            num12 = (int)((double)num12 * (num5 / 100.0));
                                            num13 = (int)((double)num13 * (num5 / 100.0));
                                            num14 = (int)((double)num14 * (num5 / 100.0));
                                        }
                                        if (num4 < num6 + 5.0)
                                        {
                                            double num17 = (num4 - num6) / 5.0;
                                            num12 = (int)((double)num12 * num17);
                                            num13 = (int)((double)num13 * num17);
                                            num14 = (int)((double)num14 * num17);
                                        }
                                        for (int m = num15; m <= num16; m++)
                                        {
                                            int num18 = l;
                                            while (num18 < l + num12 && !BadOceanCaveTiles(m, num18))
                                            {
                                                if (num18 > l + num13)
                                                {
                                                    if (WorldGen.SolidTile(m, num18, false) && Main.tile[m, num18].TileType != sandType)
                                                    {
                                                        break;
                                                    }
                                                    //Main.tile[m, num18].TileType = num3;
                                                    WorldGen.PlaceTile(m, num18, hardSandType, true, true);
                                                }
                                                else
                                                {
                                                    //Main.tile[m, num18].TileType = num2;
                                                    WorldGen.PlaceTile(m, num18, sandType, true, true);
                                                }
                                                //Main.tile[m, num18].active(true);
                                                if (WorldGen.genRand.Next(3) == 0)
                                                {
                                                    //*Main.tile[m - 1, num18].type = num2;
                                                    //Main.tile[m - 1, num18].active(true);
                                                    WorldGen.PlaceTile(m - 1, num18, hardSandType, true, true);
                                                }
                                                if (WorldGen.genRand.Next(3) == 0)
                                                {
                                                    //*Main.tile[m + 1, num18].type = num2;
                                                    //Main.tile[m + 1, num18].active(true);
                                                    WorldGen.PlaceTile(m + 1, num18, hardSandType, true, true);
                                                }
                                                num18++;
                                            }
                                        }
                                    }
                                }
                            }
                            if (num11 < num4 * 1.3 + 1.0 && l > j - 10)
                            {
                                Main.tile[k, l].LiquidAmount = 255;
                            }
                            if (flag3 && k == (int)vector2D.X && (double)l > vector2D.Y)
                            {
                                flag3 = false;
                                int num19 = 100;
                                int num20 = 2;
                                for (int n = k - num20; n <= k + num20; n++)
                                {
                                    for (int num21 = l; num21 < l + num19; num21++)
                                    {
                                        if (!BadOceanCaveTiles(n, num21))
                                        {
                                            Main.tile[n, num21].LiquidAmount = 255;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (check)
                {
                    if (vector2D2.Y > -1)
                    {
                        vector2D2.Y -= 0.03f;
                    }
                    else
                    {
                        check = false;
                    }
                }
                else if (check2)
                {
                    if (vector2D2.Y < 0)
                    {
                        vector2D2.Y += 0.07f;
                    }
                    else
                    {
                        check2 = false;
                    }
                }
                vector2D += vector2D2;
            }

            LegendGrave((int)vector2D.X, (int)vector2D.Y, 5);
        }

        private static bool BadOceanCaveTiles(int x, int y)
        {
            /*
            bool shimmerCheck = false;
            for (int i = Math.Max(x - 60, 0); i < Math.Min(x + 60, Main.maxTilesX); i++)
            {
                if (shimmerCheck)
                    break;

                for (int j = Math.Max(y - 60, 0); j < Math.Min(y + 60, Main.maxTilesY); j++)
                {
                    if (Main.tile[i, j].LiquidType == LiquidID.Shimmer)
                    {
                        shimmerCheck = true;
                        break;
                    }
                }
            }
            */

            return Main.wallDungeon[(int)(Main.tile[x, y].WallType)] || Main.tileDungeon[(int)(Main.tile[x, y].TileType)] || Main.tile[x, y].TileType == TileID.DemonAltar || Main.tile[x, y].TileType == TileID.ShadowOrbs || Main.tile[x, y].LiquidType == LiquidID.Shimmer;
        }

    }
    public class JungleStoneShrineGenPass(string name, float loadWeight) : GenPass(name, loadWeight)
    {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = JoostWorld.LegendShrineGenPassMessage.Value;

            if (!Main.remixWorld)
            {
                bool flag = true;
                while (flag)
                {
                    int i = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.6), (int)((double)Main.maxTilesX * 0.975));
                    if (GenVars.dungeonX > Main.maxTilesX / 2)
                    {
                        i = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.025), (int)((double)Main.maxTilesX * 0.4));
                    }
                    int j = WorldGen.genRand.Next((int)Main.worldSurface + 20, (int)Main.rockLayer);
                    if (Main.tile[i, j + 5].TileType == TileID.JungleGrass && !Main.tile[i, j + 4].HasTile)
                    {
                        flag = false;
                        JoostWorld.StoneShrine(i, j, TileID.IridescentBrick, WallID.JungleUnsafe3, ModContent.TileType<JungleStone>(), 4);

                        JoostMod.instance.Logger.Info("Placed Overgrowth Shrine");
                    }
                }
            }
        }
    }
    public class LegendShrinesGenPass(string name, float loadWeight) : GenPass(name, loadWeight)
    {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = JoostWorld.LegendShrineGenPassMessage.Value;

            int centerX = Main.maxTilesX / 2;
            int centerY = Main.maxTilesY / 2;
            JoostWorld.LegendShrine(centerX, centerY, Main.remixWorld);
            JoostMod.instance.Logger.Info("Placed Shrine of Legends");
            if (!Main.remixWorld)
            {
                bool flag2 = true;
                while (flag2)
                {
                    int x = GenVars.dungeonX + WorldGen.genRand.Next(200) - 100;
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
                        JoostWorld.StoneShrine(x, y, Main.tile[x, y + 5].TileType, wallType, ModContent.TileType<SkullStone>(), 19);

                        JoostMod.instance.Logger.Info("Placed Skull Shrine");
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
                        JoostWorld.StoneShrine(a, b, TileID.HellstoneBrick, WallID.HellstoneBrickUnsafe, ModContent.TileType<InfernoStone>(), 2);

                        JoostMod.instance.Logger.Info("Placed Inferno Shrine");
                    }
                }
            }
        }
    }
    public class LegendGravesGenPass(string name, float loadWeight) : GenPass(name, loadWeight)
    {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = JoostWorld.LegendGravesGenPassMessage.Value;

            int centerX = Main.maxTilesX / 2;
            int centerY = Main.maxTilesY / 2;
            JoostWorld.GraverobberOutpost(centerX + 7, centerY - 6);

            JoostWorld.LegendGrave(Main.dungeonX + 4, Main.dungeonY - 5, 0); //David
            JoostMod.instance.Logger.Info("Placed David's grave");

            bool placedLarkus = false;
            while (!placedLarkus) // Larkus
            {
                int a = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.05f), (int)(Main.maxTilesX * 0.3f));
                if (Main.rand.NextBool(2))
                {
                    a = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.7f), (int)(Main.maxTilesX * 0.95f));
                }
                int b = Main.maxTilesY - 150 + WorldGen.genRand.Next(50);
                if (Main.tile[a, b].HasTile && Main.tile[a, b + 5].HasTile && 
                    Main.tileSolid[Main.tile[a, b].TileType] && Main.tileSolid[Main.tile[a, b + 5].TileType] && 
                    (!Main.tile[a, b - 1].HasTile || !Main.tile[a + 5, b - 1].HasTile) && 
                    Main.tile[a + 2, b - 3].LiquidType != LiquidID.Lava && Main.tile[a + 3, b - 2].LiquidType != LiquidID.Lava)
                {
                    placedLarkus = true;
                    JoostWorld.LegendGrave(a, b - 5, 1);
                    JoostMod.instance.Logger.Info("Placed Larkus's grave");
                }
            }

            bool placedGnunderson = false;
            while (!placedGnunderson) // Gnunderson
            {
                int b = (Main.maxTilesY - 250) - WorldGen.genRand.Next(160, 200);
                int a = WorldGen.genRand.Next(GenVars.snowMinX[b], GenVars.snowMaxX[b]);
                if (TileID.Sets.SnowBiome[Main.tile[a, b].TileType] > 0 && 
                    Main.tile[a, b].HasTile && Main.tile[a, b + 5].HasTile &&
                    Main.tileSolid[Main.tile[a, b].TileType] && Main.tileSolid[Main.tile[a, b + 5].TileType] &&
                    (!Main.tile[a, b - 1].HasTile || !Main.tile[a + 5, b - 1].HasTile) &&
                    Main.tile[a + 2, b - 3].LiquidType != LiquidID.Lava && Main.tile[a + 3, b - 2].LiquidType != LiquidID.Lava)
                {
                    placedGnunderson = true;
                    JoostWorld.LegendGrave(a, b - 5, 2);
                    JoostMod.instance.Logger.Info("Placed Gnunderson's grave");
                }
            }

            bool placedBoook = false;
            while (!placedBoook) // Boook
            {
                int b = WorldGen.genRand.Next((int)Main.rockLayer - 60, Main.maxTilesY - 250);
                int a = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.2f), (int)(Main.maxTilesX * 0.8f));
                if (Main.tile[a, b].TileType == TileID.Grass && 
                    Main.tile[a, b].HasTile && Main.tile[a, b + 5].HasTile &&
                    Main.tileSolid[Main.tile[a, b].TileType] && Main.tileSolid[Main.tile[a, b + 5].TileType] &&
                    (!Main.tile[a, b - 1].HasTile || !Main.tile[a + 5, b - 1].HasTile) &&
                    Main.tile[a + 2, b - 3].LiquidType != LiquidID.Lava && Main.tile[a + 3, b - 2].LiquidType != LiquidID.Lava)
                {
                    placedBoook = true;
                    JoostWorld.LegendGrave(a, b - 5, 3);
                    JoostMod.instance.Logger.Info("Placed Boook's grave");
                }
            }

            bool placedGrognak = false;
            while (!placedGrognak) // Grognak
            {
                int b = WorldGen.genRand.Next((int)Main.worldSurface + 150, (int)Main.rockLayer - 80);
                int a = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15f), (int)(Main.maxTilesX * 0.85f));
                if (Main.tile[a, b].TileType == TileID.Stone && 
                    Main.tile[a, b].HasTile && Main.tile[a, b + 5].HasTile &&
                    Main.tileSolid[Main.tile[a, b].TileType] && Main.tileSolid[Main.tile[a, b + 5].TileType] &&
                    (!Main.tile[a, b - 1].HasTile || !Main.tile[a + 5, b - 1].HasTile) &&
                    Main.tile[a + 2, b - 3].LiquidType != LiquidID.Lava && Main.tile[a + 3, b - 2].LiquidType != LiquidID.Lava)
                {
                    placedGrognak = true;
                    JoostWorld.LegendGrave(a, b - 5, 4);
                    JoostMod.instance.Logger.Info("Placed Grognak's grave");
                }
            }


            bool placedUC = false; // Uncle Carius
            int uc = GenVars.dungeonSide < 0 ? 1 : 0;
            for (int i = 95; i > 55; i--)
            {
                if (placedUC)
                    break;
                int num = i;
                if (uc == 1)
                {
                    num = Main.maxTilesX - i;
                }
                for (int num2 = centerY; num2 > 0; num2--)
                {
                    if (Main.tile[num, num2].TileType == TileID.Sand || Main.tile[num, num2].TileType == TileID.Ebonsand || Main.tile[num, num2].TileType == TileID.Crimsand)
                    {
                        if (Main.tile[num, num2 - 1].LiquidAmount >= 255)
                        {
                            JoostMod.instance.Logger.Info("Generating Uncle Carius ocean cave...");
                            JoostWorld.UncleCariusCave(num, num2, Main.tile[num, num2].TileType); // Uncle Carius
                            placedUC = true;
                            JoostMod.instance.Logger.Info("Placed Uncle Carius's grave");
                            break;
                        }
                    }
                }
            }
        }
    }
}