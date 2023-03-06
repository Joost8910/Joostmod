using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EarthWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Hammer");
		}
		public override void SetDefaults()
		{
			Projectile.width = 1;
			Projectile.height = 1;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}
		private int z = 0;
		public override void AI()
		{
			int x = 8 + (int)(Projectile.position.X/16)*16;
			if (x != z)
			{
				Projectile.NewProjectile(x, Projectile.position.Y, 0, 15f, Mod.Find<ModProjectile>("EarthWave1").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);			
				z = x;
			}
		}
	}
}
