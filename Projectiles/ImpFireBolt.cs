using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ImpFireBolt : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Imp Lord's Fire");
			Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 450;
			projectile.tileCollide = false;
			projectile.light = 1f;
			aiType = ProjectileID.Bullet;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
            if (!target.HasBuff(BuffID.OnFire))
            {
                target.AddBuff(BuffID.OnFire, 420, true);
            }
		}
        public override void AI()
        {
            projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 3;
            }
            Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, -projectile.velocity.X / 5, -projectile.velocity.Y / 5, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
        }
    }
}

