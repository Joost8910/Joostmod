using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class UltimateIllusion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Illusion");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 1;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 500;
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
            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 10)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, 0, 15f, Mod.Find<ModProjectile>("UltimateIllusion1").Type, Projectile.damage, 20, Projectile.owner);
                Projectile.ai[1] -= 10;
            }
            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.position.Y -= 16 * Projectile.scale;
            }
        }
    }
}
