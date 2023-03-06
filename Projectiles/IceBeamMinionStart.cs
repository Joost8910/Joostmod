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
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.tileCollide = false;
            Projectile.light = 0.75f;
            Projectile.coldDamage = true;
        }
        public override bool PreAI()
        {
            Projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Vector2 pos = Projectile.Center;
            Vector2 dir = Projectile.velocity;
            Projectile.NewProjectile(pos.X, pos.Y, dir.X, dir.Y, Mod.Find<ModProjectile>("IceBeamMinion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
            Projectile.NewProjectile(pos.X, pos.Y, dir.X, dir.Y, Mod.Find<ModProjectile>("IceBeamMinion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(pos.X, pos.Y, dir.X, dir.Y, Mod.Find<ModProjectile>("IceBeamMinion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, -1);
        }
    }
}

