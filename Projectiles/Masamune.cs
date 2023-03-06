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
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 84;
            Projectile.height = 66;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Y -= 18;
            hitbox.Height += 18;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = !player.noItems && !player.CCed && Projectile.ai[0] < 9;
            if (channeling)
            {
                Projectile.ai[0]++;
            }
            else
            {
                Projectile.Kill();
            }
            float flip = (int)player.gravDir == -1 ? 6f : -1.5f;
            Projectile.direction = player.direction * (int)player.gravDir;
            Projectile.rotation = (Projectile.ai[0] + flip) * 0.0174f * 24f * Projectile.direction;
            Projectile.position = (player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f) + new Vector2((float)Math.Cos(Projectile.rotation + 1.566 + (2.349 * Projectile.direction)) * 45, (float)Math.Sin(Projectile.rotation + 1.566 + (2.349 * Projectile.direction)) * 45);
            Projectile.spriteDirection = Projectile.direction;
            return false;
        }

    }
}