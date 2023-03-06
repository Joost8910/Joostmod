using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SuperMissile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Missile");
		}
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(Projectile.Center - Projectile.velocity, 1, 1, 127, 0, 0, 0, default(Color), 2f);
            Main.dust[dustIndex].noGravity = true;
        }
        public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0, Projectile.velocity.Y * 0, Mod.Find<ModProjectile>("Explosion").Type, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
			SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.position.X, (int)Projectile.position.Y,  Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileExplode"));				
		}
	}
}

