using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class SandKunai : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Kunai");
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 330;
            AIType = ProjectileID.Bullet;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 10;
            height = 10;
            return true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X += 5;
            hitbox.Y += 5;
            hitbox.Width = 14;
            hitbox.Height = 14;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 100);
                Main.dust[dustIndex].noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            return base.OnTileCollide(oldVelocity);
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 300)
            {
                if (Projectile.velocity.Y < 15)
                    Projectile.velocity.Y += 0.4f;
                Projectile.velocity.X *= 0.98f;
                Projectile.rotation = Projectile.timeLeft * Projectile.direction * 0.0174f * -45f;
            }
        }
    }
}
