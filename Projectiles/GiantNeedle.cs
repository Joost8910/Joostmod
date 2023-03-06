using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GiantNeedle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Needle");
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 60f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
                Projectile.velocity.X = Projectile.velocity.X = 0.99f;

            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft -= 30;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

    }
}

