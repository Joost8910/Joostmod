using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class FireballExplosion : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Blast");
			Main.projFrames[Projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.width = 184;
			Projectile.height = 184;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 6;
			Projectile.tileCollide = false;
			Projectile.light = 1f;
            Projectile.alpha = 150;
			AIType = ProjectileID.Bullet;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 900);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 900);
        }
        public override void AI()
		{
            Projectile.frame = 6 - Projectile.timeLeft;
            if (Projectile.timeLeft == 4)
            {
                Projectile.NewProjectile(Projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, Projectile.damage / 6, 5, Projectile.owner);
            }
        }
    }
}
