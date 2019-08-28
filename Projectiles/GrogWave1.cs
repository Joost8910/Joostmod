using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GrogWave1 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
		}
		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
            projectile.extraUpdates = 1;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Bullet;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 2;
			height = 2;
			fallThrough = false;
			return true;
		}
		public override void Kill(int timeLeft)
		{
			Vector2 posi = new Vector2(projectile.position.X, projectile.position.Y+4);
			Point pos = posi.ToTileCoordinates();
			Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
			if (tileSafely.active())
			{
				Tile tileSafely2 = Framing.GetTileSafely(pos.X, pos.Y - 1);
				if (!tileSafely2.active() || !Main.tileSolid[(int)tileSafely2.type] || Main.tileSolidTop[(int)tileSafely2.type])
				{
					for (int d = 0; d < 3; d++)
					{
						Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
						dust.velocity.Y = (dust.velocity.Y - 5) * Main.rand.NextFloat();
					}
					Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);	
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 32 + (int)(-56 * projectile.ai[1]), 0, 0, mod.ProjectileType("GrogWave2"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);					
				}
			}
		}
	}
}
