using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Naginata : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Naginata");
        }
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 58;
            Projectile.scale = 1f;
            Projectile.aiStyle = 19;
            Projectile.timeLeft = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 7;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Main.player[Projectile.owner].direction = Projectile.direction;
            Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
            Main.player[Projectile.owner].itemTime = Main.player[Projectile.owner].itemAnimation;
            Projectile.position.X = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - Projectile.width / 2;
            Projectile.position.Y = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - Projectile.height / 2;
            Projectile.position += Projectile.velocity * Projectile.ai[0];
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
            }
            if (Projectile.timeLeft < 10)
            {
                Projectile.ai[0] -= 1.9f;
            }
            else
            {
                Projectile.ai[0] += 1.9f;
            }
            if (Main.player[Projectile.owner].itemAnimation == 0)
            {
                Projectile.Kill();
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
        }
    }
}