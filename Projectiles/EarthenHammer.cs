using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EarthenHammer : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Hammer");
		}
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.ThrowingKnife;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}
		public override void Kill(int timeLeft)
		{
        	for (int i = 0; i < 40; i++)
			{
				int dustType = 1;
				int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-10, 10)*0.3f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -6)*0.4f;
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 70);	
			Projectile.NewProjectile(projectile.Center.X, projectile.position.Y - 16, 4f, 0f, mod.ProjectileType("EarthWave"), (int)(projectile.damage), projectile.knockBack, projectile.owner);					
			Projectile.NewProjectile(projectile.Center.X, projectile.position.Y - 16, -4f, 0f, mod.ProjectileType("EarthWave"), (int)(projectile.damage), projectile.knockBack, projectile.owner);					
		}
	}
}


