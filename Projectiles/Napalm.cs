using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Napalm : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Napalm");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 26;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 150;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Shuriken;
		}
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 7f, 0f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 7f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -7f, 0f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -7f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, 5f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, -5f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, 5f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, -5f, 85, (int)(projectile.damage * 1f), projectile.knockBack, projectile.owner);

			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 34);				
		}
	}
}

