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
			projectile.width = 6;
			projectile.height = 6;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.extraUpdates = 2;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

		public override void AI()
		{
			int dustType = 51;
			if (projectile.owner == Main.myPlayer)
			{
				Convert((int)(projectile.position.X + (float)(projectile.width / 2)) / 16, (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16, 2);
			}
			if (projectile.timeLeft > 133)
			{
				projectile.timeLeft = 133;
			}
			if (projectile.ai[0] > 7f)
			{
				float dustScale = 1f;
				if (projectile.ai[0] == 8f)
				{
					dustScale = 0.2f;
				}
				else if (projectile.ai[0] == 9f)
				{
					dustScale = 0.4f;
				}
				else if (projectile.ai[0] == 10f)
				{
					dustScale = 0.6f;
				}
				else if (projectile.ai[0] == 11f)
				{
					dustScale = 0.8f;
				}
				projectile.ai[0] += 1f;
				for (int i = 0; i < 1; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
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
				projectile.ai[0] += 1f;
			}
			projectile.rotation += 0.3f * (float)projectile.direction;
		}

		public void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
					{
						int type = (int)Main.tile[k, l].type;
						int wall = (int)Main.tile[k, l].wall;
						if (wall == 28 || wall == 200 || wall == 202 || wall == 83 || wall == 193 || wall == 194 || wall == 3 || wall == 189 || wall == 191 || wall == 1 || wall == 61 || wall == 187 || (wall >= 54 && wall <= 58) || (wall >= 212 && wall <= 215) || (wall >= 220 && wall <= 222))
						{
							Main.tile[k, l].wall = 71;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (wall == 70 || wall == 201 || wall == 203 || wall == 81 || wall == 192 || wall == 195 || wall == 69 || wall == 188 || wall == 190 || wall == 2 ||wall == 16 || (wall >= 216 && wall <= 219 ) || (wall >= 63 && wall <= 65) ||wall == 59 || wall == 170 || wall == 171 || wall == 185 || (wall >= 196 && wall <= 199))
						{
							Main.tile[k, l].wall = 40;
							WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}


						if (type == 0 || type == 2 || type == 23 || type == 53 || type == 109 || type == 112 || type == 116 || type == 199 || type == 234 || type == 397)
						{
							Main.tile[k, l].type = 147;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 1 || type == 396 || (type >= 179 && type <= 183))
						{
							Main.tile[k, l].type = 161;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 25 || type == 400)
						{
							Main.tile[k, l].type = 163;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 203 || type == 401)
						{
							Main.tile[k, l].type = 200;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 117 || type == 403)
						{
							Main.tile[k, l].type = 164;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 404 || type == 123)
						{
							Main.tile[k, l].type = 224;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == 196)
						{
							Main.tile[k, l].type = 460;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
	
					}
				}
			}
		}
	}
}
