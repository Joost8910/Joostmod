using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DragonBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Blast");
            Main.projFrames[Projectile.type] = 16;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 128;
			Projectile.height = 128;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 140;
			Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
            Projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6, 0, 0, 0, default, 2f).noGravity = true;
            Lighting.AddLight(Projectile.Center, 2f, 1.2f, 0f);
            if (Projectile.timeLeft > 70)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            }
            else
            {
                Projectile.velocity *= 0.96f;
            }
            if (Projectile.timeLeft > 135)
            {
                Projectile.frame = 0;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 130)
            {
                Projectile.frame = 1;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 125)
            {
                Projectile.frame = 2;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 120)
            {
                Projectile.frame = 3;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 110)
            {
                Projectile.frame = 4;
                Projectile.tileCollide = true;
            }
            else if (Projectile.timeLeft > 100)
            {
                Projectile.frame = 5;
            }
            else if (Projectile.timeLeft > 90)
            {
                Projectile.frame = 6;
            }
            else if (Projectile.timeLeft > 80)
            {
                Projectile.frame = 7;
            }
            else if (Projectile.timeLeft > 70)
            {
                Projectile.frame = 8;
            }
            else if (Projectile.timeLeft > 60)
            {
                Projectile.frame = 9;
                SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);
            }
            else if (Projectile.timeLeft > 50)
            {
                Projectile.frame = 10;
            }
            else if (Projectile.timeLeft > 40)
            {
                Projectile.frame = 11;
            }
            else if (Projectile.timeLeft > 30)
            {
                Projectile.frame = 12;
            }
            else if (Projectile.timeLeft > 20)
            {
                Projectile.frame = 13;
            }
            else if (Projectile.timeLeft > 10)
            {
                Projectile.frame = 14;
            }
            else
            {
                Projectile.frame = 15;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            if (Projectile.timeLeft > 70)
                Projectile.timeLeft = 70;
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 34;
            height = 34;
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
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
                Main.dust[dustIndex].noGravity = true;
            }
        }
    }
}

