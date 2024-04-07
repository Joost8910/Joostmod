using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class UltimateIllusion4 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Illusion");
        }
        public override void SetDefaults()
        {
            Projectile.width = 164;
            Projectile.height = 300;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new Rectangle((int)Projectile.position.X + 16, (int)Projectile.position.Y, 128, 280);
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 150, 0, 0, ModContent.ProjectileType<UltimateIllusion5>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }

    }
}
