//using Terraria;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework.Input;
//using Terraria.ID;
//using Microsoft.Xna.Framework;
//using Terraria.GameContent.Generation;
//using Terraria.WorldBuilding;
//using JoostMod.Tiles;
//using Terraria.DataStructures;
//using ReLogic.Utilities;
//using System;

//namespace JoostMod
//{
//    public class WorldGenTutorialWorld : ModSystem
//    {
//        public static bool JustPressed(Keys key)
//        {
//            return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
//        }

//        public override void PostUpdateWorld()
//        {
//            if (JustPressed(Keys.D1))
//                TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
//        }

//        private void TestMethod(int x, int y)
//        {
//            Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, Color.YellowGreen, null);

//            // Code to test placed here:
//            int centerX = Main.maxTilesX / 2;

//            int centerY = Main.maxTilesY / 2;
//            //LegendShrine(x, y);
//            UncleCariusCave(x, y);
//        }
//        private static void LegendShrine(int x, int y, bool isBroken = false)
//        {
//            int stoneType = ModContent.TileType<AncientMossyStone>();
//            for (int i = x - 3; i <= x + 4; i++)
//            {
//                WorldGen.KillTile(i, y + 3);
//                WorldGen.PlaceTile(i, y + 3, stoneType, true, true);

//                WorldGen.KillTile(i, y - 3);
//                WorldGen.PlaceTile(i, y - 3, stoneType, true, true);
//                if (isBroken)
//                {
//                    WorldGen.paintTile(i, y + 3, PaintID.BrownPaint);
//                    WorldGen.paintTile(i, y - 3, PaintID.BrownPaint);
//                }

//                for (int j = y - 2; j <= y + 2; j++)
//                {
//                    Tile ti = Main.tile[i, j];
//                    ti.HasTile = false;
//                    ti.LiquidAmount = 0;
//                    ti.Slope = 0;
//                    ti.IsHalfBlock = false;
//                    WorldGen.KillWall(i, j);
//                    WorldGen.PlaceWall(i, j, 54);

//                    WorldGen.paintWall(i, j, isBroken ? PaintID.OrangePaint : PaintID.DeepTealPaint);
//                }
//            }
//            WorldGen.KillTile(x - 4, y + 3);
//            WorldGen.PlaceTile(x - 4, y + 3, stoneType, true, true);

//            WorldGen.KillTile(x + 5, y + 3);
//            WorldGen.PlaceTile(x + 5, y + 3, stoneType, true, true);

//            WorldGen.KillTile(x - 3, y - 2);
//            WorldGen.KillWall(x - 3, y - 2);
//            WorldGen.PlaceTile(x - 3, y - 2, stoneType, true, true);
//            Tile t = Main.tile[x - 3, y - 2];
//            t.Slope = SlopeType.SlopeUpLeft;

//            WorldGen.KillTile(x + 4, y - 2);
//            WorldGen.KillWall(x + 4, y - 2);
//            WorldGen.PlaceTile(x + 4, y - 2, stoneType, true, true);
//            t = Main.tile[x + 4, y - 2];
//            t.Slope = SlopeType.SlopeUpRight;

//            WorldGen.KillTile(x - 4, y - 2);
//            WorldGen.PlaceTile(x - 4, y - 2, stoneType, true, true);
//            t = Main.tile[x - 4, y - 2];
//            t.Slope = SlopeType.SlopeDownRight;

//            WorldGen.KillTile(x - 4, y - 1);
//            WorldGen.PlaceTile(x - 4, y - 1, stoneType, true, true);

//            t = Main.tile[x - 3, y - 3];
//            t.Slope = SlopeType.SlopeDownRight;

//            WorldGen.KillTile(x + 5, y - 2);
//            WorldGen.PlaceTile(x + 5, y - 2, stoneType, true, true);
//            t = Main.tile[x + 5, y - 2];
//            t.Slope = SlopeType.SlopeDownLeft;

//            WorldGen.KillTile(x + 5, y - 1);
//            WorldGen.PlaceTile(x + 5, y - 1, stoneType, true, true);

//            t = Main.tile[x + 4, y - 3];
//            t.Slope = SlopeType.SlopeDownLeft;

//            if (isBroken)
//            {
//                WorldGen.paintTile(x - 4, y + 3, PaintID.BrownPaint);
//                WorldGen.paintTile(x + 5, y + 3, PaintID.BrownPaint);
//                WorldGen.paintTile(x - 3, y - 2, PaintID.BrownPaint);
//                WorldGen.paintTile(x + 4, y - 2, PaintID.BrownPaint);
//                WorldGen.paintTile(x - 4, y - 2, PaintID.BrownPaint);
//                WorldGen.paintTile(x - 4, y - 1, PaintID.BrownPaint);
//                WorldGen.paintTile(x + 5, y - 2, PaintID.BrownPaint);
//                WorldGen.paintTile(x + 5, y - 1, PaintID.BrownPaint);
//            }

//            int type = isBroken ? ModContent.TileType<BrokenShrine>() : ModContent.TileType<ShrineOfLegends>();

//            WorldGen.PlaceObject(x, y + 2, type);
//        }

//        private void UncleCariusCave(int i, int j)
//        {
//            if (GenVars.numOceanCaveTreasure >= GenVars.maxOceanCaveTreasure)
//            {
//                GenVars.numOceanCaveTreasure = 0;
//            }
//            Vector2D vector2D = default(Vector2D);
//            vector2D.X = (double)i;
//            vector2D.Y = (double)j;
//            Vector2D vector2D2 = default(Vector2D);
//            if (i < Main.maxTilesX / 2)
//            {
//                vector2D2.X = 0.25 + WorldGen.genRand.NextDouble() * 0.25;
//            }
//            else
//            {
//                vector2D2.X = -0.35 - WorldGen.genRand.NextDouble() * 0.5;
//            }
//            vector2D2.Y = 0.4 + WorldGen.genRand.NextDouble() * 0.25;
//            ushort num = 264;
//            ushort sandType = TileID.Sandstone;
//            ushort hardSandType = TileID.HardenedSand;
//            double num4 = (double)WorldGen.genRand.Next(17, 25);
//            double num5 = (double)WorldGen.genRand.Next(500, 700);
//            double num6 = 4.0;
//            bool flag = true;
//            while (num4 > num6 && num5 > 0.0)
//            {
//                bool flag2 = true;
//                bool flag3 = true;
//                bool flag4 = true;
//                if (vector2D.X > (double)(WorldGen.beachDistance - 50) && vector2D.X < (double)(Main.maxTilesX - WorldGen.beachDistance + 50))
//                {
//                    num4 *= 0.96;
//                    num5 *= 0.96;
//                }
//                if (num4 < num6 + 2.0 || num5 < 20.0)
//                {
//                    flag4 = false;
//                }
//                if (flag)
//                {
//                    num4 -= 0.01 + WorldGen.genRand.NextDouble() * 0.01;
//                    num5 -= 0.5;
//                }
//                else
//                {
//                    num4 -= 0.02 + WorldGen.genRand.NextDouble() * 0.02;
//                    num5 -= 1.0;
//                }
//                int num7 = (int)(vector2D.X - num4 * 3.0);
//                int num8 = (int)(vector2D.X + num4 * 3.0);
//                int num9 = (int)(vector2D.Y - num4 * 3.0);
//                int num10 = (int)(vector2D.Y + num4 * 3.0);
//                if (num7 < 1)
//                {
//                    num7 = 1;
//                }
//                if (num8 > Main.maxTilesX - 1)
//                {
//                    num8 = Main.maxTilesX - 1;
//                }
//                if (num9 < 1)
//                {
//                    num9 = 1;
//                }
//                if (num10 > Main.maxTilesY - 1)
//                {
//                    num10 = Main.maxTilesY - 1;
//                }
//                for (int k = num7; k < num8; k++)
//                {
//                    for (int l = num9; l < num10; l++)
//                    {
//                        if (!BadOceanCaveTiles(k, l))
//                        {
//                            double num11 = new Vector2D(Math.Abs((double)k - vector2D.X), Math.Abs((double)l - vector2D.Y)).Length();
//                            if (flag4 && num11 < num4 * 0.5 + 1.0)
//                            {
//                                Main.tile[k, l].TileType = num;
//                                Main.tile[k, l].ClearTile();
//                            }
//                            else if (num11 < num4 * 1.5 + 1.0 && Main.tile[k, l].TileType != num)
//                            {

//                                if ((double)l < vector2D.Y)
//                                {
//                                    if ((vector2D2.X < 0.0 && (double)k < vector2D.X) || (vector2D2.X > 0.0 && (double)k > vector2D.X))
//                                    {
//                                        if (num11 < num4 * 1.1 + 1.0)
//                                        {
//                                            Main.tile[k, l].TileType = hardSandType;
//                                            if (Main.tile[k, l].LiquidAmount == 255)
//                                            {
//                                                Main.tile[k, l].WallType = 0;
//                                            }
//                                        }
//                                        else if (Main.tile[k, l].TileType != hardSandType)
//                                        {
//                                            Main.tile[k, l].TileType = sandType;
//                                        }
//                                    }
//                                }
//                                else if ((vector2D2.X < 0.0 && k < i) || (vector2D2.X > 0.0 && k > i))
//                                {
//                                    if (Main.tile[k, l].LiquidAmount == 255)
//                                    {
//                                        Main.tile[k, l].TileType = 0;
//                                    }
//                                    WorldGen.PlaceTile(k, l, sandType, true, true);
//                                    //Main.tile[k, l].TileType = num2;
//                                    //Main.tile[k, l].active(true);
//                                    if (k == (int)vector2D.X & flag2)
//                                    {
//                                        flag2 = false;
//                                        int num12 = 30 + WorldGen.genRand.Next(3);
//                                        int num13 = 23 + WorldGen.genRand.Next(3);
//                                        int num14 = 10 + WorldGen.genRand.Next(3);
//                                        int num15 = k;
//                                        int num16 = k + num14;
//                                        if (vector2D2.X < 0.0)
//                                        {
//                                            num15 = k - num14;
//                                            num16 = k;
//                                        }
//                                        if (num5 < 100.0)
//                                        {
//                                            num12 = (int)((double)num12 * (num5 / 100.0));
//                                            num13 = (int)((double)num13 * (num5 / 100.0));
//                                            num14 = (int)((double)num14 * (num5 / 100.0));
//                                        }
//                                        if (num4 < num6 + 5.0)
//                                        {
//                                            double num17 = (num4 - num6) / 5.0;
//                                            num12 = (int)((double)num12 * num17);
//                                            num13 = (int)((double)num13 * num17);
//                                            num14 = (int)((double)num14 * num17);
//                                        }
//                                        for (int m = num15; m <= num16; m++)
//                                        {
//                                            int num18 = l;
//                                            while (num18 < l + num12 && !BadOceanCaveTiles(m, num18))
//                                            {
//                                                if (num18 > l + num13)
//                                                {
//                                                    if (WorldGen.SolidTile(m, num18, false) && Main.tile[m, num18].TileType != sandType)
//                                                    {
//                                                        break;
//                                                    }
//                                                    //Main.tile[m, num18].TileType = num3;
//                                                    WorldGen.PlaceTile(m, num18, hardSandType, true, true);
//                                                }
//                                                else
//                                                {
//                                                    //Main.tile[m, num18].TileType = num2;
//                                                    WorldGen.PlaceTile(m, num18, sandType, true, true);
//                                                }
//                                                //Main.tile[m, num18].active(true);
//                                                if (WorldGen.genRand.Next(3) == 0)
//                                                {
//                                                    //*Main.tile[m - 1, num18].type = num2;
//                                                    //Main.tile[m - 1, num18].active(true);
//                                                    WorldGen.PlaceTile(m - 1, num18, hardSandType, true, true);
//                                                }
//                                                if (WorldGen.genRand.Next(3) == 0)
//                                                {
//                                                    //*Main.tile[m + 1, num18].type = num2;
//                                                    //Main.tile[m + 1, num18].active(true);
//                                                    WorldGen.PlaceTile(m + 1, num18, hardSandType, true, true);
//                                                }
//                                                num18++;
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                            if (num11 < num4 * 1.3 + 1.0 && l > j - 10)
//                            {
//                                Main.tile[k, l].LiquidAmount = 255;
//                            }
//                            if (flag3 && k == (int)vector2D.X && (double)l > vector2D.Y)
//                            {
//                                flag3 = false;
//                                int num19 = 100;
//                                int num20 = 2;
//                                for (int n = k - num20; n <= k + num20; n++)
//                                {
//                                    for (int num21 = l; num21 < l + num19; num21++)
//                                    {
//                                        if (!BadOceanCaveTiles(n, num21))
//                                        {
//                                            Main.tile[n, num21].LiquidAmount = 255;
//                                        }
//                                        else
//                                        {
//                                            num4 *= 0.99;
//                                            num5 *= 0.98;
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//                vector2D += vector2D2;
//                vector2D2.X += WorldGen.genRand.NextDouble() * 0.1 - 0.05;
//                vector2D2.Y += WorldGen.genRand.NextDouble() * 0.1 - 0.05;
//                if (flag)
//                {
//                    if (vector2D.Y > (Main.worldSurface * 2.0 + Main.rockLayer) / 3.0 && vector2D.Y > (double)(j + 30))
//                    {
//                        flag = false;
//                    }
//                    vector2D2.Y = Utils.Clamp<double>(vector2D2.Y, 0.35, 1.0);
//                }
//                else
//                {
//                    if (vector2D.X < (double)(Main.maxTilesX / 2))
//                    {
//                        if (vector2D2.X < 0.5)
//                        {
//                            vector2D2.X += 0.02;
//                        }
//                    }
//                    else if (vector2D2.X > -0.5)
//                    {
//                        vector2D2.X -= 0.02;
//                    }
//                    if (!flag4)
//                    {
//                        if (vector2D2.Y < 0)
//                        {
//                            vector2D2.Y *= 0.95;
//                        }
//                        vector2D2.Y += 0.04;
//                    }
//                    else if (vector2D.Y < (Main.worldSurface * 4.0 + Main.rockLayer) / 5.0)
//                    {
//                        if (vector2D2.Y < 0.0)
//                        {
//                            vector2D2.Y *= 0.97;
//                        }
//                        vector2D2.Y += 0.02;
//                    }
//                    else if (vector2D2.Y > -0.1)
//                    {
//                        vector2D2.Y *= 0.99;
//                        vector2D2.Y -= 0.01;
//                    }
//                    vector2D2.Y = Utils.Clamp<double>(vector2D2.Y, -1.0, 1.0);
//                }
//                if (vector2D.X < (double)(Main.maxTilesX / 2))
//                {
//                    vector2D2.X = Utils.Clamp<double>(vector2D2.X, 0.1, 1.0);
//                }
//                else
//                {
//                    vector2D2.X = Utils.Clamp<double>(vector2D2.X, -1.0, -0.1);
//                }

//            }

//            vector2D -= vector2D2 * 6;
//            bool check = true;
//            bool check2 = true;
//            while (check || check2)
//            {
//                //WorldGen.digTunnel(vector2D.X, vector2D.Y, vector2D2.X, vector2D2.Y, 1, (int)num4 * 3);
//                bool flag2 = true;
//                bool flag3 = true;
//                int num7 = (int)(vector2D.X - num4 * 3.0);
//                int num8 = (int)(vector2D.X + num4 * 3.0);
//                int num9 = (int)(vector2D.Y - num4 * 3.0);
//                int num10 = (int)(vector2D.Y + num4 * 3.0);
//                if (num7 < 1)
//                {
//                    num7 = 1;
//                }
//                if (num8 > Main.maxTilesX - 1)
//                {
//                    num8 = Main.maxTilesX - 1;
//                }
//                if (num9 < 1)
//                {
//                    num9 = 1;
//                }
//                if (num10 > Main.maxTilesY - 1)
//                {
//                    num10 = Main.maxTilesY - 1;
//                }
//                for (int k = num7; k < num8; k++)
//                {
//                    for (int l = num9; l < num10; l++)
//                    {
//                        if (!BadOceanCaveTiles(k, l))
//                        {
//                            double num11 = new Vector2D(Math.Abs((double)k - vector2D.X), Math.Abs((double)l - vector2D.Y)).Length();
//                            if (num11 < num4 * 0.5 + 1.0)
//                            {
//                                Main.tile[k, l].TileType = num;
//                                Main.tile[k, l].ClearTile();
//                            }
//                            else if (num11 < num4 * 1.5 + 1.0 && Main.tile[k, l].TileType != num)
//                            {

//                                if ((double)l < vector2D.Y)
//                                {
//                                    if ((vector2D2.X < 0.0 && (double)k < vector2D.X) || (vector2D2.X > 0.0 && (double)k > vector2D.X))
//                                    {
//                                        if (num11 < num4 * 1.1 + 1.0)
//                                        {
//                                            Main.tile[k, l].TileType = hardSandType;
//                                            if (Main.tile[k, l].LiquidAmount == 255)
//                                            {
//                                                Main.tile[k, l].WallType = 0;
//                                            }
//                                        }
//                                        else if (Main.tile[k, l].TileType != hardSandType)
//                                        {
//                                            Main.tile[k, l].TileType = hardSandType;
//                                        }
//                                    }
//                                }
//                                else if ((vector2D2.X < 0.0 && k < i) || (vector2D2.X > 0.0 && k > i))
//                                {
//                                    if (Main.tile[k, l].LiquidAmount == 255)
//                                    {
//                                        Main.tile[k, l].TileType = 0;
//                                    }
//                                    WorldGen.PlaceTile(k, l, hardSandType, true, true);
//                                    //Main.tile[k, l].TileType = num2;
//                                    //Main.tile[k, l].active(true);
//                                    if (k == (int)vector2D.X & flag2)
//                                    {
//                                        flag2 = false;
//                                        int num12 = 50 + WorldGen.genRand.Next(3);
//                                        int num13 = 43 + WorldGen.genRand.Next(3);
//                                        int num14 = 20 + WorldGen.genRand.Next(3);
//                                        int num15 = k;
//                                        int num16 = k + num14;
//                                        if (vector2D2.X < 0.0)
//                                        {
//                                            num15 = k - num14;
//                                            num16 = k;
//                                        }
//                                        if (num5 < 100.0)
//                                        {
//                                            num12 = (int)((double)num12 * (num5 / 100.0));
//                                            num13 = (int)((double)num13 * (num5 / 100.0));
//                                            num14 = (int)((double)num14 * (num5 / 100.0));
//                                        }
//                                        if (num4 < num6 + 5.0)
//                                        {
//                                            double num17 = (num4 - num6) / 5.0;
//                                            num12 = (int)((double)num12 * num17);
//                                            num13 = (int)((double)num13 * num17);
//                                            num14 = (int)((double)num14 * num17);
//                                        }
//                                        for (int m = num15; m <= num16; m++)
//                                        {
//                                            int num18 = l;
//                                            while (num18 < l + num12 && !BadOceanCaveTiles(m, num18))
//                                            {
//                                                if (num18 > l + num13)
//                                                {
//                                                    if (WorldGen.SolidTile(m, num18, false) && Main.tile[m, num18].TileType != sandType)
//                                                    {
//                                                        break;
//                                                    }
//                                                    //Main.tile[m, num18].TileType = num3;
//                                                    WorldGen.PlaceTile(m, num18, hardSandType, true, true);
//                                                }
//                                                else
//                                                {
//                                                    //Main.tile[m, num18].TileType = num2;
//                                                    WorldGen.PlaceTile(m, num18, sandType, true, true);
//                                                }
//                                                //Main.tile[m, num18].active(true);
//                                                if (WorldGen.genRand.Next(3) == 0)
//                                                {
//                                                    //*Main.tile[m - 1, num18].type = num2;
//                                                    //Main.tile[m - 1, num18].active(true);
//                                                    WorldGen.PlaceTile(m - 1, num18, hardSandType, true, true);
//                                                }
//                                                if (WorldGen.genRand.Next(3) == 0)
//                                                {
//                                                    //*Main.tile[m + 1, num18].type = num2;
//                                                    //Main.tile[m + 1, num18].active(true);
//                                                    WorldGen.PlaceTile(m + 1, num18, hardSandType, true, true);
//                                                }
//                                                num18++;
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                            if (num11 < num4 * 1.3 + 1.0 && l > j - 10)
//                            {
//                                Main.tile[k, l].LiquidAmount = 255;
//                            }
//                            if (flag3 && k == (int)vector2D.X && (double)l > vector2D.Y)
//                            {
//                                flag3 = false;
//                                int num19 = 100;
//                                int num20 = 2;
//                                for (int n = k - num20; n <= k + num20; n++)
//                                {
//                                    for (int num21 = l; num21 < l + num19; num21++)
//                                    {
//                                        if (!BadOceanCaveTiles(n, num21))
//                                        {
//                                            Main.tile[n, num21].LiquidAmount = 255;
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }

//                if (check)
//                {
//                    if (vector2D2.Y > -1)
//                    {
//                        vector2D2.Y -= 0.03f;
//                    }
//                    else
//                    {
//                        check = false;
//                    }
//                }
//                else if (check2)
//                {
//                    if (vector2D2.Y < 0)
//                    {
//                        vector2D2.Y += 0.07f;
//                    }
//                    else
//                    {
//                        check2 = false;
//                    }
//                }
//                vector2D += vector2D2;
//            }

//            LegendGrave((int)vector2D.X, (int)vector2D.Y, 5);
//        }
//        private static bool BadOceanCaveTiles(int x, int y)
//        {
//            return Main.wallDungeon[(int)(Main.tile[x, y].WallType)] || Main.tile[x, y].TileType == TileID.Stone || Main.tile[x, y].LiquidType == LiquidID.Shimmer || Main.tileDungeon[(int)(Main.tile[x, y].TileType)] || Main.tile[x, y].TileType == TileID.DemonAltar || Main.tile[x, y].TileType == TileID.ShadowOrbs;
//        }
//        private void LegendGrave(int x, int y, int graveType)
//        {
//            WorldUtils.Gen(new Point(x + 1, y), new Shapes.Rectangle(4, 1), Actions.Chain(new GenAction[]
//            {
//                new Actions.ClearTile(true)
//            }));
//            WorldUtils.Gen(new Point(x, y + 1), new Shapes.Rectangle(6, 4), Actions.Chain(new GenAction[]
//            {
//                new Actions.ClearTile(true)
//            }));

//            ushort type = (ushort)ModContent.TileType<AncientStone>();
//            WorldUtils.Gen(new Point(x + 1, y + 3), new Shapes.Rectangle(4, 1), Actions.Chain(new GenAction[]
//            {
//                new Actions.PlaceTile(type),
//                new Actions.SetFrames(true)
//            }));
//            WorldUtils.Gen(new Point(x, y + 4), new Shapes.Rectangle(6, 1), Actions.Chain(new GenAction[]
//            {
//                new Actions.PlaceTile(type),
//                new Actions.SetFrames(true)
//            }));

//            WorldGen.PlaceObject(x + 2, y + 2, ModContent.TileType<LegendGrave>(), true, graveType);
//            //graveLocations[graveType] = new PosData<Point16>(graveType, new Point16(x + 2, y + 2));
//        }
//    }
//}