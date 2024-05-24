using Microsoft.Build.Tasks.Deployment.ManifestUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class GravebusterSeeds : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Weather Star - Slime Rain");
            // Tooltip.SetDefault("'Throw it at a wall and see if it sticks!'");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 22;
            Item.height = 22;
            Item.scale = 0.25f;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true; 
            Item.value = 15;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Grass;
            //Item.shoot = ModContent.ProjectileType<Projectiles.Gravebuster>();
            //Item.shootSpeed = 0;
            Item.autoReuse = true;
        }
        public override void HoldItem(Player player)
        {
            if (Main.SmartCursorWanted)
            {
                Player.tileRangeX = 60;
                Player.tileRangeY = 40;
            }
        }
        public override bool CanUseItem(Player player)
        {
            List<Point> pList = new List<Point>();
            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.type == ModContent.ProjectileType<Projectiles.Gravebuster>())
                {
                    pList.Add(proj.Center.ToTileCoordinates());
                    //return false;
                }
            }

            Point tPos = Main.MouseWorld.ToTileCoordinates();
            if (Main.HasSmartInteractTarget)
            {
                foreach (Point p in Main.SmartInteractTileCoords)
                {
                    Tile t = Main.tile[p.X, p.Y];
                    if (t.TileType == TileID.Tombstones)
                    {
                        Point p2 = p;
                        if (t.TileFrameX % 36 < 18)
                        {
                            p2.X++;
                        }
                        if (t.TileFrameY % 36 >= 18)
                        {
                            p2.Y--;
                        }
                        if (!pList.Contains(p2))
                        {
                            tPos = p2;
                            break;
                        }
                    }
                }
                foreach (Point p in Main.SmartInteractTileCoordsSelected)
                {
                    Tile t = Main.tile[p.X, p.Y];
                    if (t.TileType == TileID.Tombstones)
                    {
                        Point p2 = p;
                        if (t.TileFrameX % 36 < 18)
                        {
                            p2.X++;
                        }
                        if (t.TileFrameY % 36 >= 18)
                        {
                            p2.Y--;
                        }
                        if (!pList.Contains(p2))
                        {
                            tPos = p2;
                            break;
                        }
                    }
                }
            }
            Tile tile = Main.tile[tPos.X, tPos.Y];
            if (tile.TileType == TileID.Tombstones)
            {
                if (tile.TileFrameX % 36 < 18)
                {
                    tPos.X++;
                }
                if (tile.TileFrameY % 36 >= 18)
                {
                    tPos.Y--;
                }
            }
            else
            {
                return false;
            }
            if (pList.Contains(tPos)) return false;

            Projectile.NewProjectile(player.GetSource_ItemUse(Item), tPos.ToWorldCoordinates(0, 0), Vector2.Zero, ModContent.ProjectileType<Projectiles.Gravebuster>(), 0, 0, player.whoAmI);
            player.ConsumeItem(Type); 
            return base.CanUseItem(player);
        }
        public override bool CanShoot(Player player)
        {
            return false;
        }
        /*
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Point tPos = Main.MouseWorld.ToTileCoordinates();
            Tile tile = Main.tile[tPos.X, tPos.Y];
            if (tile.TileType == TileID.Tombstones)
            {
                if (tile.TileFrameX % 36 < 18)
                {
                    tPos.X++;
                }
                if (tile.TileFrameY % 36 >= 18)
                {
                    tPos.Y--;
                }
            }
            position = new Vector2(tPos.X * 16, tPos.Y * 16);
        }
        */
    }
}

