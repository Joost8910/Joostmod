using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class Sparkle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparkle");
        }
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 105;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = false;
            Projectile.alpha = 128;
            AIType = ProjectileID.Bullet;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 90)
            {
                Projectile.velocity = Vector2.Zero;
            }
            if (Projectile.timeLeft < 90)
            {
                Projectile.velocity.X += Main.rand.NextFloat(-0.1f, 0.1f);
                Projectile.velocity.Y += Main.rand.NextFloat(-0.1f, 0.1f);
                Projectile.scale = Projectile.timeLeft / 90f;
            }
        }
    }
}
