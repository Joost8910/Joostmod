using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class ThornyCactus : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorny Cactus");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            AIType = ProjectileID.Shuriken;
        }
        public override void PostAI()
        {
            Projectile.spriteDirection = -Projectile.direction;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 18;
            height = 18;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            float spread = (float)Math.PI * 2;
            int baseSpeed = 6;
            double startAngle = (float)Math.PI / 2;
            double deltaAngle = spread / 8;
            double offsetAngle;
            int i;
            for (i = 0; i < 8; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("CactusThorn").Type, (int)(Projectile.damage * 0.6f), Projectile.knockBack/3, Projectile.owner);
            }
        }
    }
}

