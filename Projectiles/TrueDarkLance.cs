using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TrueDarkLance : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Dark Lance");
		}
		public override void SetDefaults()
		{
			projectile.width = 52;
			projectile.height = 52;
			projectile.scale = 1.1f;
			projectile.aiStyle = 19;
			projectile.timeLeft = 90;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.light = 0.5f;
			projectile.ownerHitCheck = true;
			projectile.hide = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}
		
		public override void AI()
		{
			Main.player[projectile.owner].direction = projectile.direction;
			Main.player[projectile.owner].heldProj = projectile.whoAmI;
			Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
			projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
			projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2);
			projectile.position += projectile.velocity * projectile.ai[0]; if (projectile.ai[0] == 0f)
			{
				projectile.ai[0] = 3f;
				projectile.netUpdate = true;
			}
			if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 2)
            {
                if (projectile.ai[1] == 0)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity, mod.ProjectileType("TrueDarkLanceBeam"), projectile.damage, projectile.knockBack / 2, projectile.owner);
                    projectile.ai[1]++;
                }
                projectile.ai[0] -= 2f;
			}
			else
			{
				projectile.ai[0] += 2f;
			}
			if (Main.player[projectile.owner].itemAnimation == 0)
			{
				projectile.Kill();
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= 1.57f;
			}
		}
	}
}