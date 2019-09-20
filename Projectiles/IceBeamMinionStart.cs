using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class IceBeamMinionStart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
        }
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 80;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.tileCollide = false;
            projectile.light = 0.75f;
            projectile.coldDamage = true;
        }
        public override bool PreAI()
        {
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Vector2 pos = projectile.Center;
            Vector2 dir = projectile.velocity;
            Projectile.NewProjectile(pos.X, pos.Y, dir.X, dir.Y, mod.ProjectileType("IceBeamMinion"), projectile.damage, projectile.knockBack, projectile.owner, 1);
            Projectile.NewProjectile(pos.X, pos.Y, dir.X, dir.Y, mod.ProjectileType("IceBeamMinion"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(pos.X, pos.Y, dir.X, dir.Y, mod.ProjectileType("IceBeamMinion"), projectile.damage, projectile.knockBack, projectile.owner, -1);
        }
    }
}

