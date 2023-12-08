using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class LeafHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leaf");
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 2 == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 2, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 1f);
                Main.dust[num1].noGravity = true;
            }
            double deg = Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Projectile.position.X = host.Center.X - (int)(Math.Cos(rad) * (400 - Projectile.timeLeft) * 1.2f) - Projectile.width / 2;
            Projectile.position.Y = host.Center.Y - (int)(Math.Sin(rad) * (400 - Projectile.timeLeft) * 1.2f) - Projectile.height / 2;
            Projectile.rotation = (float)rad;
            Projectile.ai[1] += 2f / ((401 - Projectile.timeLeft) * 1.2f * 3.14f / 360);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 2, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 1f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}

