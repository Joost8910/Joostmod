using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class PowerSpirit : MultiHoverShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit of Power");
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
			projectile.width = 22;
			projectile.height = 22;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			inertia = 15f;
			chaseAccel = 15f;
			chaseDist = 50f;
			spacingMult = 0.75f;
			shoot = mod.ProjectileType("PowerBlast");
			shootSpeed = 10f;
			shootCool = 150f;
			shootNum = 8;
			shootSpread = 360f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.dead)
			{
				modPlayer.powerSpirit = false;
			}
			if (modPlayer.powerSpirit)
			{
				projectile.timeLeft = 2;
			}
		}
		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}
	}
}


