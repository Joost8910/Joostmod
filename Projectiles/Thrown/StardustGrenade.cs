using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class StardustGrenade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Grenade");
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 5;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 150f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
                Projectile.velocity.X = Projectile.velocity.X = 0.99f;

            }

        }

        public override void Kill(int timeLeft)
        {
            var source = Projectile.GetSource_Death();
            for (int i = 0; i < 5; i++)
            {
                Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-10, 11) * .15f, Main.rand.Next(-10, -5) * .05f, Mod.Find<ModProjectile>("StardustGrenadeFragment").Type, (int)(Projectile.damage * 1f), 7, Projectile.owner);
            }
        }

    }
}


