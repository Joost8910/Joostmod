using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DragonToothWave2 : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plunging Attack");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 180;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
            projectile.melee = true;
			aiType = ProjectileID.Bullet;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 50;
        }
        public override void AI()
        {
            if (projectile.velocity.Y < 20)
            {
                projectile.velocity.Y += 0.3f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.knockBackResist > 0 && projectile.velocity.Y < 0)
            {
                target.velocity.Y -= projectile.knockBack * target.knockBackResist;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback && projectile.velocity.Y < 0)
            {
                target.velocity.Y -= projectile.knockBack;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int d = 0; d < 4; d++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 1);
            }
        }
	}
}
