using Terraria;
using Terraria.ID;

namespace JoostMod.Projectiles.Minions
{
	public class fishMinion : HoverShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pufferfish");
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 48;
			projectile.height = 60;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			inertia = 20f;
            shootCool = 120f;
            shoot = mod.ProjectileType("Bubble");
			shootSpeed = 1f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>(mod);
			if (player.dead)
			{
				modPlayer.fishMinion = false;
			}
			if (modPlayer.fishMinion)
			{
				projectile.timeLeft = 2;
			}
		}
		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}
	}
}


