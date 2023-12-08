using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Yonade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Valor);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yonade");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 12f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 11f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (crit)
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center, Projectile.DirectionTo(target.Center), ProjectileID.Grenade, (int)(Projectile.damage * 2.6f), Projectile.knockBack * 2.6f, Projectile.owner);
                Projectile.Kill();
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (crit)
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.Center, Projectile.DirectionTo(target.Center), ProjectileID.Grenade, (int)(Projectile.damage * 2.6f), Projectile.knockBack * 2.6f, Projectile.owner);
                Projectile.Kill();
            }
        }
    }
}
