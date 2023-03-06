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
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 27;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 150;
		}
		public override void AI()
		{
			if ((Projectile.timeLeft % 5) == 0)
			{
				int num1 = Dust.NewDust(
						Projectile.position,
						Projectile.width,
						Projectile.height,
						58, //Dust ID
						Projectile.velocity.X,
						Projectile.velocity.Y,
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

