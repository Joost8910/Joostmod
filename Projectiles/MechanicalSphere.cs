using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class MechanicalSphere : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Sphere");
		}
		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 34;
			projectile.aiStyle = 18;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.alpha = 5;
			projectile.light = 0.5f;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.DeathSickle;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{

			
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
			
			return false;
		}

public override void AI()
{
    for(int i = 0; i < 200; i++)
    {
       //Enemy NPC variable being set
       NPC target = Main.npc[i];

       //Getting the shooting trajectory
       float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
       float shootToY = target.position.Y - projectile.Center.Y;
       float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

       //If the distance between the projectile and the live target is active
       if(distance < 480f && !target.friendly && target.active && target.type != 488 && !target.dontTakeDamage && Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1))
       {
           if(projectile.ai[0] > 90f) //Assuming you are already incrementing this in AI outside of for loop
           {
               //Dividing the factor of 3f which is the desired velocity by distance
               distance = 3f / distance;
   
               //Multiplying the shoot trajectory with distance times a multiplier if you so choose to
               shootToX *= distance * 5;
               shootToY *= distance * 5;
   
               //Shoot projectile and set ai back to 0
               Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, 88, (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f); //Spawning a projectile
               Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, 104, (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f); //Spawning a projectile
               Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, 270, (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f); //Spawning a projectile
               Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 12);
               projectile.ai[0] = 0f;
           }
       }
    }
    projectile.ai[0] += 1f;
}

	}
}


