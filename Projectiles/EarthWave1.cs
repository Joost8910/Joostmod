using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EarthWave1 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Hammer");
		}
		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 180;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Bullet;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}
public override void Kill(int timeLeft)
		{
			Vector2 posi = new Vector2(projectile.position.X, projectile.position.Y+4);
			Point pos = posi.ToTileCoordinates();
			Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
			if (tileSafely.active())
			{
				Tile tileSafely2 = Framing.GetTileSafely(pos.X, pos.Y - 1);
				if ((!tileSafely2.active() || !Main.tileSolid[(int)tileSafely2.type] || Main.tileSolidTop[(int)tileSafely2.type]))
				{
					for (int d = 0; d < 6; d++)
					{
						Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
						dust.velocity.Y = (dust.velocity.Y - 5) * Main.rand.NextFloat();
					}
					Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);	
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 8, 0, 0, mod.ProjectileType("EarthWave2"), projectile.damage, projectile.knockBack, projectile.owner);					
				}
			}
		}
	}
}
