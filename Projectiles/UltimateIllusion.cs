using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class UltimateIllusion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultimate Illusion");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
			projectile.aiStyle = 1;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 240;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}
		public override bool CanHitPlayer(Player target)
		{
			return false;
		}
		public override void AI()
		{
			projectile.ai[1] += 1f;
			if (projectile.ai[1] >= 8f)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 0, 15f, mod.ProjectileType("UltimateIllusion1"), projectile.damage, 20, projectile.owner);
				projectile.ai[1] -= 8f;				
			}
		}

}
}
