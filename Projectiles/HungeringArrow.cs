using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class HungeringArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hungering Arrow");
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 300;
            Projectile.arrow = true;
            AIType = ProjectileID.Bullet;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(100) < 35)
            {
                Projectile.penetrate++;
            }
            else
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
        }

        public override void AI()
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] % 10 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (!target.friendly && target.type != 488 && !target.dontTakeDamage)
                    {
                        float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                        float shootToY = target.position.Y - Projectile.Center.Y;
                        float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                        //If the distance between the live targeted npc and the projectile is less than 480 pixels
                        if (distance < 200f && target.CanBeChasedBy(this, false) && !target.friendly && target.active && distance > 50f && Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
                        {
                            //Divide the factor, 3f, which is the desired velocity
                            distance = 3f / distance;

                            //Multiply the distance by a multiplier if you wish the projectile to have go faster
                            shootToX *= distance * 5;
                            shootToY *= distance * 5;

                            //Set the velocities to the shoot values
                            Projectile.velocity.X = shootToX;
                            Projectile.velocity.Y = shootToY;
                        }
                    }
                }
            }
        }


    }
}

