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
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = 27;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 12;
            projectile.timeLeft = 90;
            projectile.alpha = 75;
            projectile.light = 0.7f;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
        }
        public override void AI()
        {
            if ((projectile.timeLeft % 5) == 0)
            {
                int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 74, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
        }


    }
}

