using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class Napalm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Napalm");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Shuriken;
        }
        public override void Kill(int timeLeft)
        {
            var souce = Projectile.GetSource_Death();
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 7f, 0f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 0f, 7f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, -7f, 0f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 0f, -7f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 5f, 5f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 5f, -5f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, -5f, 5f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, -5f, -5f, 85, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);

            SoundEngine.PlaySound(SoundID.Item34, Projectile.position);
        }
    }
}

