using Terraria;
using Terraria.ID;

namespace JoostMod.Projectiles.Minions
{
	public class EmberMinion : HoverShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember");
			Main.projFrames[projectile.type] = 8;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 28;
			projectile.height = 32;
			projectile.friendly = true;
			Main.projPet[projectile.type] = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
			inertia = 20f;
            shootCool = 150f;
            shoot = mod.ProjectileType("FlameSummon");
			shootSpeed = 9f;
			chaseDist = 70f;
		}
		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>(mod);
			if (player.dead)
			{
				modPlayer.emberMinion = false;
			}
			if (modPlayer.emberMinion)
			{
				projectile.timeLeft = 2;
			}
		}
		public override void CreateDust()
		{
			if (Main.rand.Next(5) == 0)
			{	
				int dustGen = Main.rand.Next(3);
                int dustType = 158;
                if (dustGen == 1)
                {
                    dustType = 6;
                }
                if (dustGen == 2)
                {
                    dustType = 127;
                }
				Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType);
			}
		}
		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 8;
			}
		}
	}
}


