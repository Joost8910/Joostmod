using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class EnkiduMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enkidu");
			Main.projFrames[projectile.type] = 6;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 116;
			projectile.height = 100;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 0;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			inertia = 20f;
			chaseAccel = 40f;
			chaseDist = 40f;
			shoot = mod.ProjectileType("EnkiduWindFriendly");
			shootSpeed = 20f;
			shootCool = 90f;
			shootNum = 3;
			shootSpread = 45f;
            predict = true;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.dead)
			{
				modPlayer.EnkiduMinion = false;
			}
			if (modPlayer.EnkiduMinion)
			{
				projectile.timeLeft = 2;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("EnkiduMinion")] > 1 || !modPlayer.EnkiduMinion)
            {
                projectile.Kill();
            }
        }
        public override void SelectFrame(Vector2 dir)
        {
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 6;
			}
		}
	}
}


