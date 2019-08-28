using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Boomerain : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boomerain");
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 15;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.aiStyle = 3;
			//projectile.tileCollide = false;
			if (projectile.timeLeft % 8 == 0)
			{
				Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-projectile.width/2, projectile.width/2), projectile.Center.Y, 0, 7, mod.ProjectileType("Rain"), projectile.damage, 0, projectile.owner);	
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}

	}
}

