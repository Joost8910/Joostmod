using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace JoostMod.Projectiles
{
    class Landmine : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 4;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 9000;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.ai[1] < 3)
            {
                projectile.ai[1] = 3;
                projectile.timeLeft = 3;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] < 3)
            {
                projectile.ai[1] = 3;
                projectile.timeLeft = 3;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void AI()
        {
            if (projectile.ai[1] >= 3)
            {
                projectile.tileCollide = false;
                projectile.alpha = 255;
                projectile.position = projectile.Center;
                projectile.width = 256;
                projectile.height = 256;
                projectile.position.X = projectile.position.X - (projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (projectile.height / 2);
                projectile.knockBack = 30;
            }
            projectile.velocity.Y = 5;
            return;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }
    }
}
