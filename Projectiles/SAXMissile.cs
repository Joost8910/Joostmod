using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SAXMissile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Missile");
		}
		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 1;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (Projectile.timeLeft > 550)
            {
                int dustIndex = Dust.NewDust(Projectile.Center - Projectile.velocity, 1, 1, 127, 0, 0, 0, default(Color), 2f);
                Main.dust[dustIndex].noGravity = true;
            }
            else
            {
                Projectile.velocity.Y += 0.3f;
                if (Projectile.velocity.Y > 10)
                {
                    Projectile.velocity.Y = 10;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.mount.Dismount(target);
                target.velocity.Y = 10;
            }
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("SAXExplosion2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);				
		}
	}
}

