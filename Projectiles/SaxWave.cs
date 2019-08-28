using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SaxWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SA-X");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 150;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
            projectile.extraUpdates = 1;
		}
		public override bool CanHitPlayer(Player target)
		{
			return false;
		}
		private int z = 0;
		public override void AI()
		{
			int x = 8 + (int)(projectile.position.X/16)*16;
			if (x != z)
			{
				Projectile.NewProjectile(x, projectile.position.Y, 0, 15f, mod.ProjectileType("SaxWave1"), projectile.damage, projectile.knockBack, projectile.owner);			
				z = x;
			}
		}
	}
}
