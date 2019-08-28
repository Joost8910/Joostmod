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
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 150;
            aiType = ProjectileID.Shuriken;
        }
        public override void PostAI()
        {
            projectile.spriteDirection = -projectile.direction;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
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
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("CactusThorn"), (int)(projectile.damage * 0.6f), projectile.knockBack/3, projectile.owner);
            }
        }
    }
}

