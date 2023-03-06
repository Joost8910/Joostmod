using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TemperateSolution : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Temperate Spray");
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.friendly = true;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.extraUpdates = 2;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			int dustType = 0;
			if (Projectile.owner == Main.myPlayer)
			{
				Convert((int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16, (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16, 2);
			}
			if (Projectile.timeLeft > 133)
			{
				Projectile.timeLeft = 133;
			}
			if (Projectile.ai[0] > 7f)
			{
				float dustScale = 1f;
				if (Projectile.ai[0] == 8f)
				{
					dustScale = 0.2f;
				}
				else if (Projectile.ai[0] == 9f)
				{
					dustScale = 0.4f;
				}
				else if (Projectile.ai[0] == 10f)
				{
					dustScale = 0.6f;
				}
				else if (Projectile.ai[0] == 11f)
				{
					dustScale = 0.8f;
				}
				Projectile.ai[0] += 1f;
				for (int i = 0; i < 1; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
					Dust dust = Main.dust[dustIndex];
					dust.noGravity = true;
					dust.scale *= 1.75f;
					dust.velocity.X = dust.velocity.X * 2f;
					dust.velocity.Y = dust.velocity.Y * 2f;
					dust.scale *= dustScale;
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
			}
			Projectile.rotation += 0.3f * (float)Projectile.direction;
		}

		public void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
					{
						int type = (int)Main.tile[k, l].TileType;
						int wall = (int)Main.tile[k, l].WallType;
						if (wall == 187 || wall == 71)
						{
							Main.tile[k, l].WallType = 1;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 216 || wall == 40)
						{
							Main.tile[k, l].WallType = 2;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 217)
						{
							Main.tile[k, l].WallType = 188;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 218)
						{
							Main.tile[k, l].WallType = 195;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 219)
						{
							Main.tile[k, l].WallType = 203;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 220)
						{
							Main.tile[k, l].WallType = 3;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 221)
						{
							Main.tile[k, l].WallType = 83;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 222)
						{
							Main.tile[k, l].WallType = 28;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}



						if (type == 53 || (type == 147 && ((int)Main.tile[k, l-1].TileType == 5 || !Main.tile[k, l-1].HasTile || !Main.tile[k-1, l].HasTile|| !Main.tile[k+1, l].HasTile || !Main.tile[k, l+1].HasTile) ))
						{
							Main.tile[k, l].TileType = 2;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 112)
						{
							Main.tile[k, l].TileType = 23;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 116)
						{
							Main.tile[k, l].TileType = 109;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 234)
						{
							Main.tile[k, l].TileType = 199;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 396 || type == 161)
						{
							Main.tile[k, l].TileType = 1;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if ((type >= 397 && type <= 399) || type == 402 || (type == 147 && ((int)Main.tile[k, l-1].TileType != 5 && Main.tile[k, l-1].HasTile && Main.tile[k-1, l].HasTile && Main.tile[k+1, l].HasTile && Main.tile[k, l+1].HasTile) ))
						{
							Main.tile[k, l].TileType = 0;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 400 || type == 163)
						{
							Main.tile[k, l].TileType = 25;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 203 || type == 401)
						{
							Main.tile[k, l].TileType = 203;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 403 || type == 164)
						{
							Main.tile[k, l].TileType = 117;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 224 || type == 404)
						{
							Main.tile[k, l].TileType = 123;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 460)
						{
							Main.tile[k, l].TileType = 196;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
					}
				}
			}
		}
	}
}
