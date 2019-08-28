using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BitterEnd : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bitter End");
			Main.projFrames[projectile.type] = 18;
		}
		public override void SetDefaults()
		{
			projectile.width = 124;
			projectile.height = 124;
			projectile.aiStyle = 0;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 180;
			projectile.tileCollide = false;
			projectile.light = 0.95f;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Bullet;
		}
public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 10)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 18;
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 15);			
			}
		}
		public override bool CanHitPlayer(Player target)
		{
			return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void Kill(int timeLeft)
		{
		    int shootNum = 36;
			float shootSpread = 360f;
			float spread = shootSpread * 0.0174f;
			float baseSpeed = (float)Math.Sqrt(7f * 7f + 7f * 7f);
			double startAngle = Math.Atan2(7f, 7f)- spread/shootNum;
			double deltaAngle = spread/shootNum;
			double offsetAngle;
			int i;
			for (i = 0; i < shootNum;i++ )
			{
				offsetAngle = startAngle + deltaAngle * i;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), mod.ProjectileType("BitterEnd2"), projectile.damage, projectile.knockBack, projectile.owner);
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);				
		}

	}
}
