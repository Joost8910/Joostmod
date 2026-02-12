using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Dusts
{
	public class SAXMuzzleFlash : ModDust
    {
        public override void OnSpawn(Dust dust)
		{
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 120, 122);
            dust.scale = 1f;
        }

		public override bool Update(Dust dust)
        {
            if (dust.frame.Y < 122 * 4)
            {
                dust.fadeIn++;
                if (dust.fadeIn >= 3)
                {
                    dust.fadeIn = 0;
                    dust.frame.Y += 122;
                }
            }
            else
            {
                dust.alpha += 25;
                if (dust.alpha >= 255)
                {
                    dust.active = false;
                }
            }
            if (!dust.noLight)
            {
                float l = (255 - dust.alpha) / 255f * 0.25f;
                Lighting.AddLight(dust.position, l, l, l);
            }
            return false;
		}
        /*
		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return Color.White;
		}
		*/
        public override bool PreDraw(Dust dust)
        {
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}");
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height * 0.5f) / 5);
            Color color = Color.White;
            color *= (255 - dust.alpha) / 255f;
            Main.EntitySpriteDraw(tex, dust.position - Main.screenPosition, new Rectangle?(dust.frame), color, dust.rotation, drawOrigin, dust.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}
