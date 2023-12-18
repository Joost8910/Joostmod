using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class Larpoon : ModProjectile
    {
        private int ai1 = 0;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Harpoon);
            Projectile.alpha = 125;
            Projectile.light = 0.5f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Larpoon");
        }
        public override bool PreDrawExtras()
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
            // Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/Larpoon_Chain");

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
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
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
                }
            }

            return true;
        }
        public override void PostAI()
        {
            for (int i = 0; i < 200; i++)
            {
                //Enemy NPC variable being set
                NPC target = Main.npc[i];

                //Getting the shooting trajectory
                float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                //If the distance between the projectile and the live target is active
                if (distance < 480f && !target.friendly && target.active)
                {
                    if (ai1 > 6) //Assuming you are already incrementing this in AI outside of for loop
                    {
                        //Dividing the factor of 3f which is the desired velocity by distance
                        distance = 3f / distance;

                        //Multiplying the shoot trajectory with distance times a multiplier if you so choose to
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;

                        //Shoot projectile and set ai back to 0
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, 440, (int)(Projectile.damage * 1f), 0, Projectile.owner, 0f, 0f); //Spawning a projectile
                        SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
                        ai1 = 0;
                    }
                }
            }
            ai1 += 1;
        }

    }
}


