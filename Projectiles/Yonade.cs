using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Yonade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Valor);
            projectile.width = 14;
            projectile.height = 14;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yonade");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 12f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 11f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (crit)
            {
                Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(target.Center), ProjectileID.Grenade, (int)(projectile.damage * 2.6f), projectile.knockBack * 2.6f, projectile.owner);
                projectile.Kill();
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (crit)
            {
                Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(target.Center), ProjectileID.Grenade, (int)(projectile.damage * 2.6f), projectile.knockBack * 2.6f, projectile.owner);
                projectile.Kill();
            }
        }
    }
}
