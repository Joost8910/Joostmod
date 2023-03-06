using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Leaf : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf");
	        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 400;
            //projectile.tileCollide = false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            hitDirection = target.Center.X < player.Center.X ? -1 : 1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if ((int)(Projectile.ai[0] / 10) % 2 == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 2, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default(Color), 1f);
                Main.dust[num1].noGravity = true;
            }
            double deg = (double)Projectile.ai[0];
            double rad = deg * (Math.PI / 180);
            double dist = 40;
            if (Projectile.ai[1] >= 1)
            {
                Projectile.localAI[0] += Projectile.velocity.X;
                Projectile.localAI[1] += Projectile.velocity.Y;
                Projectile.netUpdate = true;
                Projectile.ownerHitCheck = false;
                if (Collision.SolidCollision(new Vector2(Projectile.localAI[0] - 5, Projectile.localAI[1] - 5), 10, 10))
                {
                    Projectile.Kill();
                }
            }
            else
            {
                Projectile.localAI[0] = player.Center.X;
                Projectile.localAI[1] = player.Center.Y;
                Projectile.ownerHitCheck = true;
                Projectile.timeLeft = 400;
            }
            Vector2 origin = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
            Projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            Projectile.ai[0] += 10f;
        }
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = 6;
			height = 6;
			fallThrough = true;
            return false;
        }
    }
}

