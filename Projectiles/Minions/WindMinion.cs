using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class WindMinion : HoverShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Miniature Tornado");
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
			projectile.width = 46;
			projectile.height = 58;
			projectile.friendly = true;
			Main.projPet[projectile.type] = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			inertia = 20f;
			shootCool = 40f;
			shoot = mod.ProjectileType("WindBall");
			shootSpeed = 12f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>(mod);
			if (player.dead)
			{
				modPlayer.WindMinion = false;
			}
			if (modPlayer.WindMinion)
			{
				projectile.timeLeft = 2;
			}
		}
		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 6;
			}
		}
	}
}


