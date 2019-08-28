using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace JoostMod.Projectiles.Minions
{
	public class ICUMinion : MultiHoverShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ICU");
            Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 30;
			projectile.height = 30;
            projectile.friendly = true;
			Main.projPet[projectile.type] = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
			inertia = 50f;
			shoot = mod.ProjectileType("ICUBeam");
            shootNum = 4;
            shootSpread = 360;
			shootSpeed = 10f;
			chaseDist = 130f;
		}
        public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>(mod);
			if (player.dead)
			{
				modPlayer.icuMinion = false;
			}
			if (modPlayer.icuMinion)
			{
				projectile.timeLeft = 2;
			}
        }
        float rot = 0;
        public override void SelectFrame()
        {
            rot += 22.5f * 0.0174f;
            projectile.rotation = rot * projectile.direction;
        }
    }
}


