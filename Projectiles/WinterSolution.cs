using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class WinterSolution : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Winter Spray");
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
			int dustType = 51;
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
						if (wall == 28 || wall == 200 || wall == 202 || wall == 83 || wall == 193 || wall == 194 || wall == 3 || wall == 189 || wall == 191 || wall == 1 || wall == 61 || wall == 187 || (wall >= 54 && wall <= 58) || (wall >= 212 && wall <= 215) || (wall >= 220 && wall <= 222))
						{
							Main.tile[k, l].WallType = 71;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 70 || wall == 201 || wall == 203 || wall == 81 || wall == 192 || wall == 195 || wall == 69 || wall == 188 || wall == 190 || wall == 2 ||wall == 16 || (wall >= 216 && wall <= 219 ) || (wall >= 63 && wall <= 65) ||wall == 59 || wall == 170 || wall == 171 || wall == 185 || (wall >= 196 && wall <= 199))
						{
							Main.tile[k, l].WallType = 40;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}


						if (type == 0 || type == 2 || type == 23 || type == 53 || type == 109 || type == 112 || type == 116 || type == 199 || type == 234 || type == 397)
						{
							Main.tile[k, l].TileType = 147;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 1 || type == 396 || (type >= 179 && type <= 183))
						{
							Main.tile[k, l].TileType = 161;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 25 || type == 400)
						{
							Main.tile[k, l].TileType = 163;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 203 || type == 401)
						{
							Main.tile[k, l].TileType = 200;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 117 || type == 403)
						{
							Main.tile[k, l].TileType = 164;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 404 || type == 123)
						{
							Main.tile[k, l].TileType = 224;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 196)
						{
							Main.tile[k, l].TileType = 460;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
	
					}
				}
			}
		}
	}
}
