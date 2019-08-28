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
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 60f)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.15f;
                projectile.velocity.X = projectile.velocity.X = 0.99f;

            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft -= 30;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

    }
}

