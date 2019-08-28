using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GrogWaveFlipped1 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
		}
		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 120;
            projectile.extraUpdates = 1;
            projectile.tileCollide = true;
			projectile.ignoreWater = true;
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
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);	
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 13, 0, 1, mod.ProjectileType("GrogWaveFlipped2"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);					
			return true;
		}

	}
}
