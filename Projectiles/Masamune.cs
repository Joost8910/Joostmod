using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Masamune : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Masamune");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 84;
            projectile.height = 66;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Y -= 18;
            hitbox.Height += 18;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = !player.noItems && !player.CCed && projectile.ai[0] < 9;
            if (channeling)
            {
                projectile.ai[0]++;
            }
            else
            {
                projectile.Kill();
            }
            float flip = (int)player.gravDir == -1 ? 6f : -1.5f;
            projectile.direction = player.direction * (int)player.gravDir;
            projectile.rotation = (projectile.ai[0] + flip) * 0.0174f * 24f * projectile.direction;
            projectile.position = (player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f) + new Vector2((float)Math.Cos(projectile.rotation + 1.566 + (2.349 * projectile.direction)) * 45, (float)Math.Sin(projectile.rotation + 1.566 + (2.349 * projectile.direction)) * 45);
            projectile.spriteDirection = projectile.direction;
            return false;
        }

    }
}