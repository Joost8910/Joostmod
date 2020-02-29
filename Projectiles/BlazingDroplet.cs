using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BlazingDroplet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lava Droplet");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 18;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 300;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 6;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
        public override void AI()
		{
			projectile.rotation = 0;
            if (projectile.timeLeft % 10 == 0)
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, 0, -2, 0, default, 2f).noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 3; i++)
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, 0, -10, 0, default, 2f);
        }
    }
}

