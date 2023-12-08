using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class BoneHurtingJuice : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Hurting Juice");
        }
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 200;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("BoneHurt").Type, 600, false);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("BoneHurt").Type, 600, false);
        }
    }
}

