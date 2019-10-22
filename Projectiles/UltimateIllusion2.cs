using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class UltimateIllusion2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultimate Illusion");
		}
		public override void SetDefaults()
		{
			projectile.width = 164;
			projectile.height = 48;
			projectile.aiStyle = 0;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
			projectile.light = 0.95f;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Bullet;
		}
		public override bool CanHitPlayer(Player target)
		{
			return false;
		}
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 76, projectile.velocity.X * 0, projectile.velocity.Y * 0, mod.ProjectileType("UltimateIllusion3"), (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);		
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 66);				
		}

	}
}
