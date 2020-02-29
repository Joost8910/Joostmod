using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BlazingArrow : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Arrow");
		}
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.arrow = true;
			aiType = ProjectileID.WoodenArrowFriendly;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, -projectile.velocity.X, -projectile.velocity.Y, 0, default, 2f);
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.8f, 0.1f);
            if (Main.rand.NextBool(15))
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, 0, 2, 0, default, 2f).noGravity = true;
                Projectile.NewProjectile(projectile.Center, new Vector2(0, 1), mod.ProjectileType("BlazingDroplet"), projectile.damage / 2, 0, projectile.owner);
            }
        }
    }
}

