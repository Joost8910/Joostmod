using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class fishMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pufferfish");
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
			Projectile.width = 48;
			Projectile.height = 60;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 20f;
            shootCool = 120f;
            shoot = Mod.Find<ModProjectile>("Bubble").Type;
			shootSpeed = 1f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.fishMinion = false;
			}
			if (modPlayer.fishMinion)
			{
				Projectile.timeLeft = 2;
			}
        }
        public override void SelectFrame(Microsoft.Xna.Framework.Vector2 dir)
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}
		}
	}
}


