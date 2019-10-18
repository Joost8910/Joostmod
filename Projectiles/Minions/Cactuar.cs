using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace JoostMod.Projectiles.Minions
{
	public class Cactuar : GroundShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar");
			Main.projFrames[projectile.type] = 2;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //This is necessary for right-click targetting
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 48;
			projectile.height = 59;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 40;
            inertia = 20f;
			spacingMult = 2f;
			chaseAccel = 7f;
			chaseDist = 10f;
			shootCool = 35f;
			shoot = mod.ProjectileType("Needle7");
			shootSpeed = 15f;
            predict = true;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.cactuarMinions = false;
			}
			if (modPlayer.cactuarMinions)
			{
				projectile.timeLeft = 2;
			}
		}
		public override void FlyingDust()
		{
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 93, 0f, 0f, 0, default(Color), 0.7f);
		}

		public override void SelectFrame()
		{
			projectile.frameCounter += (int)Math.Abs(projectile.velocity.X*2.5f);
			if (projectile.frameCounter >= 65)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 2;
			}
		}
    }
}