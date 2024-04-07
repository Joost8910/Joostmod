using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class ImpFireBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imp Lord's Fire");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 450;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (!target.HasBuff(BuffID.OnFire))
            {
                target.AddBuff(BuffID.OnFire, 420, true);
            }
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 3;
            }
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, -Projectile.velocity.X / 5, -Projectile.velocity.Y / 5, 100, default, 1f + Main.rand.Next(20) * 0.1f).noGravity = true;
        }
    }
}

