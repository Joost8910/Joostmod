using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class EnkiduWind : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enkidu's Wind");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.aiStyle = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 150;
            AIType = ProjectileID.Bullet;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.timeLeft > 980)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 6;
            }
        }

    }
}

