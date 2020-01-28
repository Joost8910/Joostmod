using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DragonToothWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plunging Attack");
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
            int grav = 1;
            if (projectile.velocity.Y < 0)
            {
                grav = -1;
            }
			Vector2 posi = new Vector2(projectile.position.X, projectile.position.Y+4* grav);
			Point pos = posi.ToTileCoordinates();
			Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
			if (tileSafely.active())
			{
				Tile tileSafely2 = Framing.GetTileSafely(pos.X, pos.Y - 1* grav);
				if ((!tileSafely2.active() || !Main.tileSolid[(int)tileSafely2.type] || Main.tileSolidTop[(int)tileSafely2.type]))
				{
					for (int d = 0; d < 6; d++)
					{
						Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
						dust.velocity.Y = (dust.velocity.Y - 5) * Main.rand.NextFloat() * grav;
					}
					Projectile.NewProjectile(projectile.Center.X, (pos.Y*16) - 8*grav, projectile.velocity.X*15, -6* grav, mod.ProjectileType("DragonToothWave2"), projectile.damage, projectile.knockBack, projectile.owner);					
				}
			}
		}
	}
}
