using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DragonBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Blast");
            Main.projFrames[projectile.type] = 16;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 128;
			projectile.height = 128;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 140;
			projectile.tileCollide = false;
            projectile.scale = 1f;
            projectile.ranged = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 75;
            projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, 0, 0, 0, default, 2f).noGravity = true;
            Lighting.AddLight(projectile.Center, 2f, 1.2f, 0f);
            if (projectile.timeLeft > 70)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
            }
            else
            {
                projectile.velocity *= 0.96f;
            }
            if (projectile.timeLeft > 135)
            {
                projectile.frame = 0;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 130)
            {
                projectile.frame = 1;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 125)
            {
                projectile.frame = 2;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 120)
            {
                projectile.frame = 3;
                projectile.position -= projectile.velocity;
            }
            else if (projectile.timeLeft > 110)
            {
                projectile.frame = 4;
                projectile.tileCollide = true;
            }
            else if (projectile.timeLeft > 100)
            {
                projectile.frame = 5;
            }
            else if (projectile.timeLeft > 90)
            {
                projectile.frame = 6;
            }
            else if (projectile.timeLeft > 80)
            {
                projectile.frame = 7;
            }
            else if (projectile.timeLeft > 70)
            {
                projectile.frame = 8;
            }
            else if (projectile.timeLeft > 60)
            {
                projectile.frame = 9;
                Main.PlaySound(2, projectile.Center, 20);
            }
            else if (projectile.timeLeft > 50)
            {
                projectile.frame = 10;
            }
            else if (projectile.timeLeft > 40)
            {
                projectile.frame = 11;
            }
            else if (projectile.timeLeft > 30)
            {
                projectile.frame = 12;
            }
            else if (projectile.timeLeft > 20)
            {
                projectile.frame = 13;
            }
            else if (projectile.timeLeft > 10)
            {
                projectile.frame = 14;
            }
            else
            {
                projectile.frame = 15;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            if (projectile.timeLeft > 70)
                projectile.timeLeft = 70;
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 40;
            height = 40;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1200);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1200);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 22; i++)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                Main.dust[dustIndex].noGravity = true;
            }
        }
    }
}

