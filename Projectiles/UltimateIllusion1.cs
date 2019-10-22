using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class UltimateIllusion1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultimate Illusion");
		}
		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Bullet;
		}
		public override bool CanHitPlayer(Player target)
		{
			return false;
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);	
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 0, projectile.velocity.Y * 0, mod.ProjectileType("UltimateIllusion2"), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);					
		}

	}
}
