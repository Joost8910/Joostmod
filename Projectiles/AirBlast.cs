using Microsoft.Xna.Framework;
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
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 120;
			Projectile.alpha = 150;
			AIType = ProjectileID.Bullet;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31);
        }
        public override void AI()
        {
            if (Main.rand.NextBool(3))
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 18;
            height = 18;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * Projectile.velocity.Length() / 10f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
            {
                target.velocity = Projectile.velocity;
                target.velocity.Y -= 0.4f;
            }
            Projectile.velocity *= 0.8f;
        }
    }
}

