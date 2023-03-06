using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Tornade : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornade");
		}
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 2;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 200;
			AIType = ProjectileID.Shuriken;
		}
		
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 15, 0, 0, Mod.Find<ModProjectile>("Nado").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);				
		}
	}
}

