using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class LightWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Pwnhammer");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
            projectile.extraUpdates = 1;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}
		private int z = 0;
		public override void AI()
		{
			int x = 8 + (int)(projectile.position.X/16)*16;
			if (x != z)
			{
				Projectile.NewProjectile(x, projectile.position.Y, 0, 15f * projectile.ai[0], mod.ProjectileType("LightWave1"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0]);			
				z = x;
			}
		}
	}
}
