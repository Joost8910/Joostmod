using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class SnowFlake : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Flake");
        }
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 300;
            Projectile.alpha = 95;
            AIType = ProjectileID.Bullet;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return true;
        }
        public override void AI()
        {
            var source = Projectile.GetSource_FromAI();
            Projectile.rotation = Projectile.timeLeft * -Projectile.direction;
            if (Projectile.velocity.Y < 2.5f)
            {
                Projectile.velocity.Y += 0.1f;
            }
            if (Projectile.velocity.Y > 2.5f)
            {
                Projectile.velocity.Y -= 0.2f;
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 15)
            {
                if (Main.rand.NextBool(2))
                {
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 0f, 3f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 3f, 0f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 0f, -3f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, -3f, 0f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                }
                else
                {
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 2.2f, 2.2f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, 2.2f, -2.2f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, -2.2f, -2.2f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                    Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, -2.2f, 2.2f, ProjectileID.NorthPoleSnowflake, Projectile.damage, 3, Projectile.owner);
                }
                Projectile.ai[1] -= 15;
            }
        }
    }
}
