using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class WindMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Miniature Tornado");
			Main.projFrames[Projectile.type] = 6;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 46;
			Projectile.height = 58;
			Projectile.friendly = true;
			Main.projPet[Projectile.type] = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 20f;
			shootCool = 40f;
			shoot = Mod.Find<ModProjectile>("WindBall").Type;
			shootSpeed = 12f;
            spacingMult = 0.8f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.WindMinion = false;
			}
			if (modPlayer.WindMinion)
			{
				Projectile.timeLeft = 2;
			}
        }
        public override void SelectFrame(Microsoft.Xna.Framework.Vector2 dir)
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			}
		}
	}
}


