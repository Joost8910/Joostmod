using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class IceBeamCharged : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Beam");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 8;
		}
		public override void SetDefaults()
		{
			projectile.width = 100;
			projectile.height = 100;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			projectile.tileCollide = false;
			projectile.light = 1.5f;
			aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 15;
            projectile.coldDamage = true;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(44, 600);
			
		}
public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 8;
			}
		}
	}
}
