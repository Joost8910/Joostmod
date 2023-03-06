using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Axe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Axe");
        }
        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 52;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.timeLeft * 0.0174f * 14f * -Projectile.direction;
            if (Projectile.timeLeft % 18 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item7, Projectile.Center);
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 24;
            height = 24;
            return true;
        }
        // Now this is where the chain magic happens. You don't have to try to figure this whole thing out.
        // Just make sure that you edit the first line (which starts with 'Texture2D texture') correctly.
        public override bool PreDraw(ref Color lightColor)
        {
            // So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
            // Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
            Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Projectiles/Axe_Chain");

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
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
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
        public override void Kill(int timeLeft)
        {

            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -Projectile.velocity.X / 2, -Projectile.velocity.Y / 2, Mod.Find<ModProjectile>("Axe2").Type, (int)(Projectile.damage * 0.5f), Projectile.knockBack / 3, Projectile.owner);

        }

    }
}
