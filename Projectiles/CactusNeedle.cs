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
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = 1;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 300;
			AIType = ProjectileID.Bullet;
		}
		public override void ModifyHitPlayer(Player player, ref int damage, ref bool crit)
		{
			player.GetModPlayer<JoostPlayer>().enemyIgnoreDefenseDamage = 10;
		}
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			player.immuneTime = 1;
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
		}
	}
}

