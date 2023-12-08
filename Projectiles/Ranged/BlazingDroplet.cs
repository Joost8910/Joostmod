using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class BlazingDroplet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Droplet");
        }
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 300;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void AI()
        {
            Projectile.rotation = 0;
            if (Projectile.timeLeft % 10 == 0)
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6, 0, -2, 0, default, 2f).noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6, 0, -10, 0, default, 2f);
        }
    }
}

