using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SandClump : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Ball");
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 900;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
            AIType = ProjectileID.Bullet;
            CooldownSlot = 1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.direction * -0.5f * Projectile.timeLeft;
            if (Projectile.velocity.Y > 0)
                Projectile.tileCollide = true;
            if (Projectile.velocity.Y < 5)
                Projectile.velocity.Y += 0.1f;
            Projectile.velocity.X *= 0.999f;
        }
        public override void OnKill(int timeLeft)
        {
            for(int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 32, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 2f);
                d.velocity = Projectile.DirectionTo(d.position) * 10;
            }
            var source = Projectile.GetSource_Death();
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(50);
            float damageMult = 0.8f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, -8).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<SandBall>(), (int)(Projectile.damage * damageMult), Projectile.knockBack * damageMult, Projectile.owner, 1);
            }
            /*
            numberProjectiles = 5;
            rotation = MathHelper.ToRadians(120);
            damageMult = 0.5f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(-Projectile.velocity.X, -Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.SandBallFalling, (int)(Projectile.damage * damageMult), Projectile.knockBack * damageMult, Projectile.owner);
            }
            */
        }

    }
}
