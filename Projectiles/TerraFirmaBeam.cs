using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class TerraFirmaBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Firma");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 12;
            Projectile.timeLeft = 90;
            Projectile.alpha = 75;
            Projectile.light = 0.7f;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        public override void AI()
        {
            if ((Projectile.timeLeft % 5) == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 74, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f);

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
        }


    }
}

