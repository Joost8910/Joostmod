using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class Gnome : GroundShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome");
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
			projectile.width = 20;
			projectile.height = 37;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			inertia = 20f;
			shoot = mod.ProjectileType("GnomeHat");
			shootSpeed = 10f;
			spacingMult = 0.6f;
			shootCool = 120f;
		}
		public override void FlyingDust()
		{
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 111, 0f, 0f, 0, default(Color), 0.7f);
		}
		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.Gnome = false;
			}
			if (modPlayer.Gnome)
			{
				projectile.timeLeft = 2;
			}
		}
		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 5 && projectile.ai[0] != 1f && Math.Abs(projectile.velocity.X) > 0.1f)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
			if (projectile.ai[0] == 1f)
			{
				projectile.frame = 1;
			}
			else if (Math.Abs(projectile.velocity.X) <= 0.1f)
			{
				projectile.frame = 2;
			}
		}
	}
}


