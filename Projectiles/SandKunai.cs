using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SandKunai : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Kunai");
		}
		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 330;
            aiType = ProjectileID.Bullet;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 10;
            height = 10;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X += 5;
            hitbox.Y += 5;
            hitbox.Width = 14;
            hitbox.Height = 14;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                int dustIndex = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 100);
                Main.dust[dustIndex].noGravity = true;
            }
            Main.PlaySound(0, projectile.Center);
        }
        public override void AI()
        {
            if (projectile.timeLeft < 300)
            {
                if (projectile.velocity.Y < 15)
                    projectile.velocity.Y += 0.4f;
                projectile.velocity.X *= 0.98f;
                projectile.rotation = projectile.timeLeft * projectile.direction * 0.0174f * -45f;
            }
        }
    }
}
