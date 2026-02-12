using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Dusts
{
	public class SAXSmokeCloud : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 352, 128);
			dust.scale = 1f;
		}

		public override bool Update(Dust dust)
        {
			if (dust.fadeIn == 0)
			{
				dust.alpha -= 10;
				if (dust.alpha <= 60)
				{
					dust.alpha = 60;
					dust.fadeIn = 1f;
				}
			}
			else
            {
                dust.alpha += 2;
                if (dust.alpha > 100)
                {
					if (dust.velocity.X == 0 && dust.alpha > 140)
					{
						dust.velocity.X += 0.01f * (Main.rand.Next(2) * 2 - 1);
					}	
                    if (dust.velocity.X > 0 && dust.velocity.X < 3)
                    {
                        dust.velocity.X += 0.04f;
                    }
                    if (dust.velocity.X < 0 && dust.velocity.X > -3)
                    {
                        dust.velocity.X -= 0.04f;
                    }
                }
                if (dust.alpha > 250)
                {
                    dust.active = false;
                }
            }
            dust.position += dust.velocity;
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
			Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
			Color color = Lighting.GetColor(dust.position.ToTileCoordinates());

			color *= (255 - dust.alpha) / 255f;
			Main.EntitySpriteDraw(tex, dust.position - Main.screenPosition, new Rectangle?(dust.frame), color, dust.rotation, drawOrigin, dust.scale, SpriteEffects.None);

			return false;
		}
	}
}
