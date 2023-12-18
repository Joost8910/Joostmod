using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Accessory
{
    public class Skull : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skull");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 540;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            for (int i = 0; i < 200; i++)
            {
                //Enemy NPC variable being set
                NPC target = Main.npc[i];

                //Getting the shooting trajectory
                float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                if (distance < 480f && !target.friendly && target.active && target.type != 488 && !target.dontTakeDamage)
                {
                    distance = 3f / distance;
                    shootToX *= distance * 2;
                    shootToY *= distance * 2;
                    Projectile.velocity.X = shootToX;
                    Projectile.velocity.Y = shootToY;
                    if (Projectile.ai[1] == 60)
                    {
                        if (Main.myPlayer == Projectile.owner)
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, ModContent.ProjectileType<ShadowBolt>(), Projectile.damage, Projectile.knockBack, Projectile.owner); //Spawning a projectile
                        SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                        Projectile.ai[1] = 0;
                    }
                }
            }
            if (Projectile.timeLeft >= 480)
            {
                Projectile.ai[1]++;
            }
            if (Projectile.timeLeft == 480)
            {
                Projectile.frame = 1;
            }
            if (Projectile.timeLeft < 420)
            {
                Projectile.frame = 2;
            }
            if (Projectile.timeLeft >= 420)
            {
                double deg = Projectile.ai[0];
                double rad = deg * (Math.PI / 180);
                double dist = 55;
                Player P = Main.player[Projectile.owner];
                Projectile.position.X = P.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = P.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                Projectile.ai[0] += 9f;
            }
        }
    }
}

