using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class FrozenOrb : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Orb");
		}
		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 10;
			projectile.timeLeft = 90;
			projectile.alpha = 95;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
			projectile.coldDamage = true;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 20;
			height = 20;
			return true;
		}
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, 0f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, 0f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);

			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 2.6f, 1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 2.6f, -1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -2.6f, 1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -2.6f, -1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);

			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 1.5f, 2.6f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 1.5f, -2.6f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -1.5f, 2.6f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -1.5f, -2.6f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);


		}

		public override void AI()
		{
			projectile.ai[1] += 1f;
			projectile.rotation = projectile.ai[1] * 6;
		if(projectile.ai[1] == 20f)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -2.3f, 1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 2.3f, 1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
		}
		if(projectile.ai[1] == 40f)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 1.5f, -2.3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 1.5f, 2.3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -3f, 0f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
		}
		if(projectile.ai[1] == 60f)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 2.3f, -1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -2.3f, -1.5f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
		}
		if(projectile.ai[1] == 80f)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 3f, 0f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -1.5f, -2.3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -1.5f, 2.3f, mod.ProjectileType("Icicle"), (int)(projectile.damage * 0.5f), 2, projectile.owner);
		}

		}

	}
}

