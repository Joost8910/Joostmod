using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DirtBoltSummon : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DirtBolt");
		}
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
        {
            projectile.rotation = projectile.timeLeft * -projectile.direction* 0.0174f * 20f;
            if (Main.rand.Next(8) == 0)
			{	
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 0);
			}
		}
	}
}

