using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class UltimateIllusion2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Illusion");
        }
        public override void SetDefaults()
        {
            Projectile.width = 164;
            Projectile.height = 48;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = false;
            Projectile.light = 0.95f;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 76, Projectile.velocity.X * 0, Projectile.velocity.Y * 0, ModContent.ProjectileType<UltimateIllusion3>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            SoundEngine.PlaySound(SoundID.Item66, Projectile.position);
        }

    }
}
