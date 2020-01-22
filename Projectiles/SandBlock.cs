using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SandBlock : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SandBallGun);
            projectile.thrown = true;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand");
        }
        public override void AI()
        {
            if (projectile.velocity.Y < 10)
            {
                projectile.velocity.Y += 0.13f;
            }
            projectile.rotation -= projectile.timeLeft * projectile.direction * 0.5f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            int item = -1;
            int x = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
            int y = (int)(projectile.position.Y + (float)(projectile.width / 2)) / 16;
            int tileType = TileID.Sand;
            int itemType = ItemID.SandBlock;
            if (projectile.ai[1] >= 0)
            {
                if (Main.tile[x, y].halfBrick() && projectile.velocity.Y > 0f && Math.Abs(projectile.velocity.Y) > Math.Abs(projectile.velocity.X))
                {
                    int num535 = y;
                    y = num535 - 1;
                }
                if (!Main.tile[x, y].active() && tileType >= 0)
                {
                    bool flag5 = false;
                    if (y < Main.maxTilesY - 2 && Main.tile[x, y + 1] != null && Main.tile[x, y + 1].active() && Main.tile[x, y + 1].type == 314)
                    {
                        flag5 = true;
                    }
                    if (!flag5)
                    {
                        WorldGen.PlaceTile(x, y, tileType, false, true, -1, 0);
                        if (Main.tile[x, y].active() && (int)Main.tile[x, y].type == tileType)
                        {
                            if (Main.tile[x, y + 1].halfBrick() || Main.tile[x, y + 1].slope() != 0)
                            {
                                WorldGen.SlopeTile(x, y + 1, 0);
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(17, -1, -1, null, 14, (float)x, (float)(y + 1), 0f, 0, 0, 0);
                                }
                            }
                            if (Main.netMode != 0)
                            {
                                NetMessage.SendData(17, -1, -1, null, 1, (float)x, (float)y, (float)tileType, 0, 0, 0);
                            }
                        }
                    }
                    else if (itemType > 0 && Main.rand.NextBool(3))
                    {
                        item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, itemType, 1, false, 0, false, false);
                    }
                }
                else if (itemType > 0 && Main.rand.NextBool(3))
                {
                    item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, itemType, 1, false, 0, false, false);
                }
                if (Main.netMode == 1 && item >= 0)
                {
                    NetMessage.SendData(21, -1, -1, null, item, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            else
            {
                if (itemType > 0)
                {
                    item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, itemType, 1, false, 0, false, false);
                }
                if (Main.netMode == 1 && item >= 0)
                {
                    NetMessage.SendData(21, -1, -1, null, item, 1f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}
