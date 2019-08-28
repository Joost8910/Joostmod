using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BoltofLight : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bolt of Light");
		}
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 27;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 150;
		}
		public override void AI()
		{
			if ((projectile.timeLeft % 5) == 0)
			{
				int num1 = Dust.NewDust(
						projectile.position,
						projectile.width,
						projectile.height,
						58, //Dust ID
						projectile.velocity.X,
						projectile.velocity.Y,
						100, //alpha goes from 0 to 255
						default(Color),
						1f
						);

				Main.dust[num1].noGravity = true;
				Main.dust[num1].velocity *= 0.1f;
            }
		}
	}
}

