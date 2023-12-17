using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class ShockWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Wave");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 1;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            int x = 8 + (int)(Projectile.position.X / 16) * 16;
            if (x != Projectile.localAI[1])
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0, 15f, ModContent.ProjectileType<ShockWave1>(), Projectile.damage, 7, Projectile.owner);
                Projectile.localAI[1] = x;
            }
        }
    }
}
