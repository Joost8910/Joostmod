using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class CactusSwarm : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm");
		}
		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
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
		public override void Kill(int timeLeft)
		{
			Vector2 posi = new Vector2(projectile.position.X, projectile.position.Y+4);
			Point pos = posi.ToTileCoordinates();
			Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
			if (tileSafely.active())
			{
                Main.PlaySound(15, (int)projectile.position.X, (int)projectile.position.Y, 1);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 16, projectile.velocity.X*100, 0, mod.ProjectileType("CactusWorm"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0]);					
			}
		}
	}
}
