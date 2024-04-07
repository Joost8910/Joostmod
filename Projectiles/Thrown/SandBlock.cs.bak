using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class SandBlock : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SandBallGun);
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand");
        }
        public override void AI()
        {
            if (Projectile.velocity.Y < 10)
            {
                Projectile.velocity.Y += 0.13f;
            }
            Projectile.rotation -= Projectile.timeLeft * Projectile.direction * 0.5f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            int item = -1;
            int x = (int)(Projectile.position.X + Projectile.width / 2) / 16;
            int y = (int)(Projectile.position.Y + Projectile.width / 2) / 16;
            int tileType = TileID.Sand;
            int itemType = ItemID.SandBlock;
            if (Projectile.ai[1] >= 0)
            {
                if (Main.tile[x, y].IsHalfBlock && Projectile.velocity.Y > 0f && Math.Abs(Projectile.velocity.Y) > Math.Abs(Projectile.velocity.X))
                {
                    int num535 = y;
                    y = num535 - 1;
                }
                if (!Main.tile[x, y].HasTile && tileType >= 0)
                {
                    bool flag5 = false;
                    if (y < Main.maxTilesY - 2 && Main.tile[x, y + 1] != null && Main.tile[x, y + 1].HasTile && Main.tile[x, y + 1].TileType == 314)
                    {
                        flag5 = true;
                    }
                    if (!flag5)
                    {
                        WorldGen.PlaceTile(x, y, tileType, false, true, -1, 0);
                        if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == tileType)
                        {
                            if (Main.tile[x, y + 1].IsHalfBlock || Main.tile[x, y + 1].Slope != 0)
                            {
                                WorldGen.SlopeTile(x, y + 1, 0);
                                if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 14, x, y + 1, 0f, 0, 0, 0);
                                }
                            }
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, x, y, tileType, 0, 0, 0);
                            }
                        }
                    }
                    else if (itemType > 0 && Main.rand.NextBool(3))
                    {
                        item = Item.NewItem(Projectile.GetSource_Death(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, itemType, 1, false, 0, false, false);
                    }
                }
                else if (itemType > 0 && Main.rand.NextBool(3))
                {
                    item = Item.NewItem(Projectile.GetSource_Death(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, itemType, 1, false, 0, false, false);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            else
            {
                if (itemType > 0)
                {
                    item = Item.NewItem(Projectile.GetSource_Death(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, itemType, 1, false, 0, false, false);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}
