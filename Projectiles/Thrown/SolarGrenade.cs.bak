using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class SolarGrenade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Grenade");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 5;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 150f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
                Projectile.velocity.X = Projectile.velocity.X = 0.99f;

            }

        }

        public override void Kill(int timeLeft)
        {
            var source = Projectile.GetSource_Death();
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 0f, 10f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 10f, 0f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 0f, -10f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, -10f, 0f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 7f, 7f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 7f, -7f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, -7f, 7f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
            Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, -7f, -7f, 636, (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
        }

    }
}


