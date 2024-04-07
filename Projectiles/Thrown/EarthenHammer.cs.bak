using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class EarthenHammer : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthen Hammer");
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.ThrowingKnife;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                int dustType = 1;
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-10, 10) * 0.3f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -6) * 0.4f;
            }
            SoundEngine.PlaySound(SoundID.Item70, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.position.Y - 16, 4f, 0f, ModContent.ProjectileType<EarthWave>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.position.Y - 16, -4f, 0f, ModContent.ProjectileType<EarthWave>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
    }
}


