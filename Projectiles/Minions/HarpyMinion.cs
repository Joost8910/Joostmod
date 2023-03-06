using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class HarpyMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Harpy Minion");
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 60;
			Projectile.height = 40;
			Projectile.friendly = true;
			Main.projPet[Projectile.type] = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 100f;
			shoot = Mod.Find<ModProjectile>("Feather").Type;
			shootSpeed = 9f;
            chaseDist = 250f;
            chaseAccel = 18f;
        }

		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.HarpyMinion = false;
			}
			if (modPlayer.HarpyMinion)
			{
				Projectile.timeLeft = 2;
			}
        }
        public override void SelectFrame(Microsoft.Xna.Framework.Vector2 dir)
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}
		}
	}
}


