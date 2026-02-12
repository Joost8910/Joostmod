using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Dusts
{
	public class SAXSmokePuff : ModDust
    {
        public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 32, 40);
        }

		public override bool Update(Dust dust)
        {
            dust.fadeIn++;
            if (dust.fadeIn >= 5)
            {
                dust.fadeIn = 0;
                dust.frame.Y += 40;
            }
            if (dust.frame.Y >= 40 * 4)
			{
				dust.active = false;
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
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height * 0.5f) / 4);
            Color color = Lighting.GetColor(dust.position.ToTileCoordinates());
            color *= (255 - dust.alpha) / 255f;
            Main.EntitySpriteDraw(tex, dust.position - Main.screenPosition, new Rectangle?(dust.frame), color, dust.rotation, drawOrigin, dust.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}
