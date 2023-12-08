using Microsoft.Xna.Framework;
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
			Projectile.width = 116;
			Projectile.height = 100;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 20f;
			chaseAccel = 40f;
			chaseDist = 40f;
			shoot = Mod.Find<ModProjectile>("EnkiduWindFriendly").Type;
			shootSpeed = 20f;
			shootCool = 90f;
			shootNum = 3;
			shootSpread = 45f;
            predict = true;
		}

		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.EnkiduMinion = false;
			}
			if (modPlayer.EnkiduMinion)
			{
				Projectile.timeLeft = 2;
            }
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("EnkiduMinion").Type] > 1 || !modPlayer.EnkiduMinion)
            {
                Projectile.Kill();
            }
        }
        public override void SelectFrame(Vector2 dir)
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			}
		}
	}
}


