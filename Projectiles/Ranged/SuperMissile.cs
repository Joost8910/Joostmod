using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
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
            int dustIndex = Dust.NewDust(Projectile.Center - Projectile.velocity, 1, 1, DustID.Flare, 0, 0, 0, default, 2f);
            Main.dust[dustIndex].noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0, Projectile.velocity.Y * 0, ModContent.ProjectileType<Explosion>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileExplode"), Projectile.Center);
        }
    }
}

