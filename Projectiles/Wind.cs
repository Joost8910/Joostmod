using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Wind : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wind");
            Main.projFrames[Projectile.type] = 12;
        }
		public override void SetDefaults()
		{
			Projectile.width = 160;
			Projectile.height = 50;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.timeLeft = 200;
			Projectile.alpha = 150;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            height = 12;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void AI()
        {
            Projectile.velocity.Y = 0;
            if (Projectile.timeLeft > 195)
            {
                Projectile.frame = 0;
                Projectile.position -= Projectile.velocity;
                Projectile.spriteDirection = Projectile.direction;
            }
            else if (Projectile.timeLeft > 190)
            {
                Projectile.frame = 1;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 25)
            {
                if (Projectile.timeLeft % 5 == 0)
                {
                    Projectile.frame = 2 + ((Projectile.frame - 1) % 5);
                }
                if (Projectile.timeLeft < 160)
                {
                    Projectile.tileCollide = true;
                }
            }
            else if (Projectile.timeLeft > 20)
            {
                Projectile.frame = 7;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 15)
            {
                Projectile.frame = 8;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 10)
            {
                Projectile.frame = 9;
                Projectile.position -= Projectile.velocity;
            }
            else if (Projectile.timeLeft > 5)
            {
                Projectile.frame = 10;
                Projectile.position -= Projectile.velocity;
            }
            else
            {
                Projectile.frame = 11;
                Projectile.position -= Projectile.velocity;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            if (Projectile.timeLeft > 25)
                Projectile.timeLeft = 25;
            return false;
        }
    }
}

