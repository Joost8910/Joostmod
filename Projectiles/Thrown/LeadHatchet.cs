using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class LeadHatchet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lead Hatchet");
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 22;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
            AIType = ProjectileID.Shuriken;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.Distance(player.Center) > 550)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {

            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<LeadHatchet2>(), (int)(Projectile.damage * 1f), 3, Projectile.owner);

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/Lead_Chain");

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
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

    }
}
