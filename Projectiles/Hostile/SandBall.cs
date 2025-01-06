using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SandBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Ball");
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = false;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 32, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1f);

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            Projectile.rotation = Projectile.direction * -0.5f * Projectile.timeLeft;
            if (Projectile.ai[0] > 0)
            {
                if (Projectile.velocity.Y < 10)
                    Projectile.velocity.Y += 0.15f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 32, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1f);
                d.velocity = Projectile.DirectionTo(d.position) * 2;
            }
        }

    }
}
