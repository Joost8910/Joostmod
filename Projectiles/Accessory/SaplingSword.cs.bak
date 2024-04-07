using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Accessory
{
    public class SaplingSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 9;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            Player P = Main.player[Projectile.owner];

            Projectile.position = P.Center - (Projectile.Size / 2);
            Projectile.position += Projectile.velocity * Projectile.ai[1];
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 3f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] < 4f)
            {
                Projectile.ai[1] -= 2f;
            }
            else
            {
                Projectile.ai[1] += 1.5f;
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
            Projectile.ai[0] -= P.GetAttackSpeed(DamageClass.Melee);
            if (Projectile.ai[0] > 0)
            {
                Projectile.timeLeft = 2;
            }
        }

    }
}

