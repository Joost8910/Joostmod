using Microsoft.Xna.Framework;
using Terraria;
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
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(projectile.Center - projectile.velocity, 1, 1, 127, 0, 0, 0, default(Color), 2f);
            Main.dust[dustIndex].noGravity = true;
        }
        public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 0, projectile.velocity.Y * 0, mod.ProjectileType("Explosion"), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y,  mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileExplode"));				
		}
	}
}

