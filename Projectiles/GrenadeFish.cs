using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GrenadeFish : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grenade Fish");
		}
		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 200;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.WoodenArrowFriendly;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X / 2;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y / 2;
			}
			return false;
		}
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0, Projectile.velocity.Y * 0, Mod.Find<ModProjectile>("Pop").Type, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
			SoundEngine.PlaySound(SoundID.Item54, Projectile.position);				
		}
	}
}

