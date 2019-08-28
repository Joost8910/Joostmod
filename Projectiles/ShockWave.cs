using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class ShockWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Wave");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = 1;
            projectile.hostile = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
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
            int x = 8 + (int)(projectile.position.X / 16) * 16;
            if (x != projectile.localAI[1])
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 0, 15f, mod.ProjectileType("ShockWave1"), projectile.damage, 7, projectile.owner);
                projectile.localAI[1] = x;
            }
        }
    }
}
