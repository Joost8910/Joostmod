using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Sparkle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparkle");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 105;
			projectile.light = 0.3f;
			projectile.ignoreWater = false;
			projectile.alpha = 128;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            return false;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 90)
            {
                projectile.velocity = Vector2.Zero;
            }
            if (projectile.timeLeft < 90)
            {
                projectile.velocity.X += Main.rand.NextFloat(-0.1f, 0.1f);
                projectile.velocity.Y += Main.rand.NextFloat(-0.1f, 0.1f);
                projectile.scale = projectile.timeLeft / 90f;
            }
        }
        public override void Kill(int timeLeft)
        {
            //Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Sparkle2"), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
        }


	}
}
