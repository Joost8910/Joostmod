using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TerraWave : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Firma");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 80;
            projectile.extraUpdates = 1;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        private int z = 0;
		public override void AI()
		{
			int x = 8 + (int)(projectile.position.X/16)*16;
			if (x != z)
			{
				Projectile.NewProjectile(x, projectile.position.Y, 0, 15f * projectile.ai[0], mod.ProjectileType("TerraWave1"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0]);			
				z = x;
			}
		}
	}
}
