using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class HungeringArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hungering Arrow");
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 300;
            projectile.arrow = true;
            aiType = ProjectileID.Bullet;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 6;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(100) < 35)
            {
                projectile.penetrate++;
            }
            else
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, projectile.Center);
        }

        public override void AI()
        {
            projectile.ai[1]++;
            if (projectile.ai[1] % 10 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (!target.friendly && target.type != 488 && !target.dontTakeDamage)
                    {
                        float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                        float shootToY = target.position.Y - projectile.Center.Y;
                        float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                        //If the distance between the live targeted npc and the projectile is less than 480 pixels
                        if (distance < 200f && target.CanBeChasedBy(this, false) && !target.friendly && target.active && distance > 50f && Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1))
                        {
                            //Divide the factor, 3f, which is the desired velocity
                            distance = 3f / distance;

                            //Multiply the distance by a multiplier if you wish the projectile to have go faster
                            shootToX *= distance * 5;
                            shootToY *= distance * 5;

                            //Set the velocities to the shoot values
                            projectile.velocity.X = shootToX;
                            projectile.velocity.Y = shootToY;
                        }
                    }
                }
            }
        }


    }
}

