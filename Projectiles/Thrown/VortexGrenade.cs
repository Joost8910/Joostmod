using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class VortexGrenade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Grenade");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
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
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 dir = -Projectile.velocity;
                dir.Normalize();
                dir *= 8;
                Vector2 perturbedSpeed = dir.RotatedByRandom(MathHelper.ToRadians(30));
                float scale = 1f - Main.rand.NextFloat() * .3f;
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, 616, Projectile.damage, Projectile.knockBack * 1.5f, Projectile.owner);
            }
        }

    }
}


