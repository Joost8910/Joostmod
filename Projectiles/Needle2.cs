using Microsoft.Xna.Framework;
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
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 30;
			Projectile.alpha = 255;
			AIType = ProjectileID.Bullet;
		}
        /*
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i<16; i++)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * .25f, projectile.velocity.Y * .25f, mod.ProjectileType("Needle6"), projectile.damage, projectile.knockback, projectile.owner);
			}	
		}
        */
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 16; i++)
            {
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * .25f, Projectile.velocity.Y * .25f, Mod.Find<ModProjectile>("Needle6").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 6;
            height = 6;
            return true;
        }

    }
}

