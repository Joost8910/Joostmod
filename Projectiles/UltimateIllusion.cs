using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class UltimateIllusion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Illusion");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = 1;
            projectile.hostile = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 500;
            projectile.tileCollide = false;
            aiType = ProjectileID.Bullet;
            projectile.extraUpdates = 1;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            projectile.ai[1]++;
            if (projectile.ai[1] >= 10)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 15f, mod.ProjectileType("UltimateIllusion1"), projectile.damage, 20, projectile.owner);
                projectile.ai[1] -= 10;
            }
            if (Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.position.Y -= 16 * projectile.scale;
            }
        }
    }
}
