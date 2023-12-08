using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class EternalFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Flame");
        }
        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 78;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 34;
            height = 34;
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft % 5 == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 2f);
                Main.dust[num1].noGravity = true;
            }
            if (Projectile.localAI[0] < 5)
            {
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
            }
            Projectile.rotation = Projectile.timeLeft * -12 * (float)Math.PI / 180f * Projectile.direction;
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= 5 && Projectile.localAI[1] <= 0)
            {
                if (Projectile.localAI[0] > 60)
                {
                    Projectile.velocity = Projectile.DirectionTo(player.MountedCenter) * Projectile.ai[0] * 2;
                }
                Vector2 move = player.MountedCenter - Projectile.Center;
                if (Projectile.localAI[0] == 5)
                {
                    int x = Projectile.velocity.X > 0 ? 1 : -1;
                    int y = Projectile.velocity.Y > 0 ? 1 : -1;
                    move = new Vector2(x, -y) * 100;
                }
                if (move.Length() > 12 * Projectile.ai[0] && Projectile.localAI[0] < 20)
                {
                    Projectile.localAI[0] = 20;
                }
                if (Projectile.localAI[0] > 30)
                {
                    Projectile.tileCollide = false;
                }
                float home = Projectile.localAI[0] < 20 ? 12f : 6f;
                if (move.Length() > Projectile.ai[0] && Projectile.ai[0] > 0)
                {
                    move *= Projectile.ai[0] / move.Length();
                }
                else
                {
                    Projectile.Kill();
                }
                Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;

                if (Projectile.velocity.Length() < Projectile.ai[0] && Projectile.ai[0] > 0)
                {
                    Projectile.velocity *= Projectile.ai[0] / Projectile.velocity.Length();
                }
            }
            else
            {
                Projectile.localAI[1]--;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            if (Projectile.localAI[0] <= 30)
            {
                Projectile.velocity *= 0.5f;
                Projectile.localAI[1] = 6;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            if (Projectile.localAI[0] <= 30)
            {
                Projectile.velocity *= 0.5f;
                Projectile.localAI[1] = 6;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            return false;
        }

    }
}

