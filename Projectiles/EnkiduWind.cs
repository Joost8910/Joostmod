using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EnkiduWind : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enkidu's Wind");
			Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 42;
			projectile.height = 42;
			projectile.aiStyle = 1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 150;
			aiType = ProjectileID.Bullet;
		}
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.timeLeft > 980)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 6;
			}
		}

	}
}

