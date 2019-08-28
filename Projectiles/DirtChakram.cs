using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class DirtChakram : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Copper Hatchet");
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            projectile.aiStyle = 3;
            if (Main.rand.Next(8) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 0);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
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
