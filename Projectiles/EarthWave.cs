using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EarthWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Hammer");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}
		private int z = 0;
		public override void AI()
		{
			int x = 8 + (int)(projectile.position.X/16)*16;
			if (x != z)
			{
				Projectile.NewProjectile(x, projectile.position.Y, 0, 15f, mod.ProjectileType("EarthWave1"), projectile.damage, projectile.knockBack, projectile.owner);			
				z = x;
			}
		}
	}
}
