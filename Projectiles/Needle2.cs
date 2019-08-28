using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Needle2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("1000 Needles");
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 30;
			projectile.alpha = 255;
			aiType = ProjectileID.Bullet;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i<16; i++)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * .25f, projectile.velocity.Y * .25f, mod.ProjectileType("Needle6"), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			}	
		}

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 6;
            height = 6;
            return true;
        }

    }
}

