using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class ICUMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ICU");
            Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 30;
			Projectile.height = 30;
            Projectile.friendly = true;
			Main.projPet[Projectile.type] = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
			inertia = 50f;
			shoot = Mod.Find<ModProjectile>("ICUBeam").Type;
            shootNum = 4;
            shootSpread = 360;
			shootSpeed = 10f;
			chaseDist = 130f;
		}
        public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.icuMinion = false;
			}
			if (modPlayer.icuMinion)
			{
				Projectile.timeLeft = 2;
			}
        }
        float rot = 0;
        private void SelectFrame()
        {
            rot += 22.5f * 0.0174f;
            Projectile.rotation = rot * Projectile.direction;
        }
    }
}


