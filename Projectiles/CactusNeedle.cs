using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class CactusNeedle : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("10000 Needles");
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.tileCollide = false;
			projectile.penetrate = 3;
			projectile.timeLeft = 300;
			aiType = ProjectileID.Bullet;
		}
		public override void ModifyHitPlayer(Player player, ref int damage, ref bool crit)
		{
			player.GetModPlayer<JoostPlayer>(mod).enemyIgnoreDefenseDamage = 10;
		}
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			player.immuneTime = 1;
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
		}
	}
}

