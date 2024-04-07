using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class HostileWind : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind");
            Main.projFrames[Projectile.type] = 12;
        }
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 200;
            Projectile.alpha = 50;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Y += 14;
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            height = 12;
            return true;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.timeLeft > 170 || Projectile.timeLeft < 20)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.timeLeft > 170 || Projectile.timeLeft < 20)
            {
                return false;
            }
            return base.CanHitNPC(target);
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
                    Projectile.frame = 2 + (Projectile.frame - 1) % 5;
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

