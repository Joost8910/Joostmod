using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class ShroomBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Bullet");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 399;
            projectile.alpha = 5;
            projectile.extraUpdates = 1;
            projectile.scale = 1.5f;
            aiType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            if (projectile.timeLeft % 10 == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, 131, (int)(projectile.damage * 0.5f), 0, projectile.owner);
            }
        }

    }
}

