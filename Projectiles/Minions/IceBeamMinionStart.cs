using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
    public class IceBeamMinionStart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
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
            Projectile.NewProjectile(Projectile.GetSource_Death(), pos.X, pos.Y, dir.X, dir.Y, Mod.Find<ModProjectile>("IceBeamMinion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
            Projectile.NewProjectile(Projectile.GetSource_Death(), pos.X, pos.Y, dir.X, dir.Y, Mod.Find<ModProjectile>("IceBeamMinion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_Death(), pos.X, pos.Y, dir.X, dir.Y, Mod.Find<ModProjectile>("IceBeamMinion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, -1);
        }
    }
}

