using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SAXBeamCharged : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1800;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.light = 0.75f;
            projectile.coldDamage = true;
            aiType = ProjectileID.Bullet;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X += 16;
            hitbox.Y += 16;
            hitbox.Width = 68;
            hitbox.Height = 68;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                if (!target.HasBuff(BuffID.Frozen))
                {
                    target.AddBuff(BuffID.Frozen, 100, true);
                }
            }
            target.AddBuff(BuffID.Frostburn, 600, true);
            target.AddBuff(BuffID.Chilled, 200, true);

        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 8;
            }
        }
    }
}

