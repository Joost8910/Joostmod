using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BurningSphere : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lord's Flame");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 450;
			projectile.tileCollide = true;
            projectile.alpha = 150;
            aiType = ProjectileID.Bullet;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 6;
            height = 6;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }
        public override void AI()
        {
            projectile.rotation = projectile.direction * 3 * projectile.timeLeft;
            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, 0, 0, 100, default(Color), 2f + (Main.rand.Next(5) * 0.1f)).noGravity = true;
        }
    }
}

