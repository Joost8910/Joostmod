using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class BloodWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Blood Breaker");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        private int z = 0;
        public override void AI()
        {
            int x = 8 + (int)(Projectile.position.X / 16) * 16;
            if (x != z)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), x, Projectile.position.Y, 0, 15f * Projectile.ai[0], ModContent.ProjectileType<BloodWave1>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0]);
                z = x;
            }
        }
    }
}
