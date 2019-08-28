using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class IronHatchet  : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Hatchet");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 22;
			projectile.aiStyle = 2;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 90;
			aiType = ProjectileID.Shuriken;
		}
		public override void Kill(int timeLeft)
		{

			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("IronHatchet2"), (int)(projectile.damage * 1f), 3, projectile.owner);
				
		}        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("JoostMod/Projectiles/IronFlail_Chain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
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
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }

}
}
