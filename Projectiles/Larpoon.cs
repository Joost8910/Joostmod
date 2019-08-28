using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Larpoon : ModProjectile
	{
		private int ai1 = 0;
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Harpoon);
			projectile.alpha = 125;
			projectile.light = 0.5f;
            projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;			
		}
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Larpoon");
		}
        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return false;
        }
public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            // So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
            // Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
            Texture2D texture = ModContent.GetTexture("JoostMod/Projectiles/Larpoon_Chain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
public override void PostAI()
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
       if(distance < 480f && !target.friendly && target.active)
       {
           if(ai1 > 6) //Assuming you are already incrementing this in AI outside of for loop
           {
               //Dividing the factor of 3f which is the desired velocity by distance
               distance = 3f / distance;
   
               //Multiplying the shoot trajectory with distance times a multiplier if you so choose to
               shootToX *= distance * 5;
               shootToY *= distance * 5;
   
               //Shoot projectile and set ai back to 0
               Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, 440, (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f); //Spawning a projectile
               Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 12);
               ai1 = 0;
           }
       }
    }
    ai1 += 1;
}

	}
}


