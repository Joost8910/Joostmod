using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class DirtBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DirtBolt");
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.timeLeft * -Projectile.direction * 0.0174f * 20f;
            if (Main.rand.Next(8) == 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0);
            }
        }
    }
}

