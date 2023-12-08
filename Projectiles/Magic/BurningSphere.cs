using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class BurningSphere : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lord's Flame");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 450;
            Projectile.tileCollide = true;
            Projectile.alpha = 150;
            AIType = ProjectileID.Bullet;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 6;
            height = 6;
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.direction * 3 * Projectile.timeLeft;
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 2f + Main.rand.Next(5) * 0.1f).noGravity = true;
        }
    }
}

