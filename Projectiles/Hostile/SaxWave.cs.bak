using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SaxWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SA-X");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        private int z = 0;
        public override void AI()
        {
            int x = 8 + (int)(Projectile.position.X / 16) * 16;
            if (x != z)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), x, Projectile.position.Y, 0, 15f, ModContent.ProjectileType<SaxWave1>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                z = x;
            }
        }
    }
}
