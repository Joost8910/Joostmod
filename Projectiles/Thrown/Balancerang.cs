using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class Balancerang : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Balancerang");
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 40;
            Projectile.alpha = 5;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 60f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
                Projectile.velocity.X = Projectile.velocity.X = 0.99f;

            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            return false;
        }

        public override void Kill(int timeLeft)
        {

            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * 1f, ModContent.ProjectileType<Balancerang2>(), (int)(Projectile.damage * 1f), 7, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * 1f, Main.rand.Next(-10, -5) * -1f, ModContent.ProjectileType<Balancerang3>(), (int)(Projectile.damage * 1f), 7, Projectile.owner);

        }

    }
}

