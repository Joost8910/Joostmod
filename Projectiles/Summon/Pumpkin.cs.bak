using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Summon
{
    public class Pumpkin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpkin");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            //projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1801;
            Projectile.extraUpdates = 1;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            hitDirection = target.Center.X < player.Center.X ? -1 : 1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] < 0)
            {
                Projectile.hide = true;
            }
            else
            {
                Projectile.hide = false;
                if ((int)(Projectile.ai[0] / 5f) % 10 == 0)
                {
                    int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 1f);
                    Main.dust[num1].noGravity = true;
                    Main.dust[num1].velocity *= 0.01f;
                }
            }
            if ((int)Projectile.ai[0] == 0)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    Vector2 diff = Main.MouseWorld - player.Center;
                    diff.Normalize();
                    Projectile.velocity = diff * Projectile.velocity.Length();
                    Projectile.netUpdate = true;
                }
            }
            double deg = (double)Projectile.ai[0] + 90;
            double rad = deg * (Math.PI / 180);
            double dist = 64;
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            if (Projectile.ai[1] >= 1)
            {
                Projectile.localAI[0] += Projectile.velocity.X;
                Projectile.localAI[1] += Projectile.velocity.Y;
                Projectile.netUpdate = true;
                dist = Projectile.timeLeft < 1744 ? 8 : Projectile.timeLeft - 1736;
                Projectile.rotation = (float)rad;
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
                Projectile.timeLeft = 1800;
            }
            Vector2 origin = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
            Projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.ai[0] += 5;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return Projectile.ai[0] > 0;
        }
        public override bool CanHitPvp(Player target)
        {
            return Projectile.ai[0] > 0;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 8;
            height = 8;
            fallThrough = true;
            return false;
        }
    }
}

