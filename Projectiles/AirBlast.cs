using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class AirBlast : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gust of Air");
		}
		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 34;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 120;
			projectile.alpha = 150;
			aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 31);
        }
        public override void AI()
        {
            if (Main.rand.NextBool(3))
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 31);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 18;
            height = 18;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * projectile.velocity.Length() / 10f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
            {
                target.velocity = projectile.velocity;
                target.velocity.Y -= 0.4f;
            }
            projectile.velocity *= 0.8f;
        }
    }
}

