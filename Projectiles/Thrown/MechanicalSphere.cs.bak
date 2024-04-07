using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class MechanicalSphere : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Sphere");
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = 18;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 5;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.DeathSickle;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
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
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            return false;
        }

        public override void AI()
        {
            var source = Projectile.GetSource_FromAI();
            for (int i = 0; i < 200; i++)
            {
                //Enemy NPC variable being set
                NPC target = Main.npc[i];

                //Getting the shooting trajectory
                float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                //If the distance between the projectile and the live target is active
                if (distance < 480f && !target.friendly && target.active && target.type != 488 && !target.dontTakeDamage && Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
                {
                    if (Projectile.ai[0] > 90f) //Assuming you are already incrementing this in AI outside of for loop
                    {
                        //Dividing the factor of 3f which is the desired velocity by distance
                        distance = 3f / distance;

                        //Multiplying the shoot trajectory with distance times a multiplier if you so choose to
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;

                        //Shoot projectile and set ai back to 0
                        Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, 88, (int)(Projectile.damage * 1f), 0, Projectile.owner, 0f, 0f); //Spawning a projectile
                        Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, 104, (int)(Projectile.damage * 1f), 0, Projectile.owner, 0f, 0f); //Spawning a projectile
                        Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, 270, (int)(Projectile.damage * 1f), 0, Projectile.owner, 0f, 0f); //Spawning a projectile
                        SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
                        Projectile.ai[0] = 0f;
                    }
                }
            }
            Projectile.ai[0] += 1f;
        }

    }
}


