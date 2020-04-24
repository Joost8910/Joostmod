using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
    public class FourthAnniversary : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Joostmod's Fourth Anniversary");
            Tooltip.SetDefault("'The Journey may End, but the legend never dies'");
        }
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 500000;
            item.rare = 8;
            item.createTile = mod.TileType("FourthAnniversary");
            item.placeStyle = 0;
        }
        public override void PostDrawInInventory(SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = mod.GetTexture("Items/Placeable/FourthAnniversarySolarEclipse");
            Color color = drawColor;
            float alpha = 1000;
            if (Main.eclipse)
            {
                alpha = (float)(Main.time <= 200d ? (Main.time * 5d) : 1000d);
                if (alpha < 0)
                {
                    alpha = 0;
                }
                color.A = (byte)((int)(255f * (alpha / 1000f)));
                if (alpha > 0)
                {
                    sb.Draw(tex, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                }
            }

            Texture2D tex2 = mod.GetTexture("Items/Placeable/FourthAnniversaryNight");
            color = drawColor;
            alpha = (float)(Main.dayTime ? Main.time - 53000d : (32400d - Main.time));
            if (alpha < 0)
            {
                alpha = 0;
            }
            if (alpha > 1000)
            {
                alpha = 1000;
            }
            color.A = (byte)((int)(255f * (alpha / 1000f)));
            if (alpha > 0 && !(Main.bloodMoon && Main.time > 200 && Main.time < 31400) && !(Main.eclipse && Main.time > 200 && Main.time < 53000))
            {
                sb.Draw(tex2, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
            }

            Texture2D tex3 = mod.GetTexture("Items/Placeable/FourthAnniversaryBloodMoon");
            color = drawColor;
            if (Main.bloodMoon)
            {
                alpha = (float)(Main.time <= 200d ? (Main.time * 5d) : (32400d - Main.time));
                if (alpha < 0)
                {
                    alpha = 0;
                }
                if (alpha > 1000)
                {
                    alpha = 1000;
                }
                color.A = (byte)((int)(255f * (alpha / 1000f)));
                if (alpha > 0)
                {
                    sb.Draw(tex3, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                }
            }
        }
        public override void PostDrawInWorld(SpriteBatch sb, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = mod.GetTexture("Items/Placeable/FourthAnniversarySolarEclipse");
            float x = (float)(item.width / 2f - tex.Width / 2f);
            float y = (float)(item.height - tex.Height);
            Color color = lightColor;
            float alpha = 1000;
            if (Main.eclipse)
            {
                alpha = (float)(Main.time <= 200d ? (Main.time * 5d) : 1000d);
                if (alpha < 0)
                {
                    alpha = 0;
                }
                color.A = (byte)((int)(255f * (alpha / 1000f)));
                if (alpha > 0)
                {
                    sb.Draw(tex, new Vector2(item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }

            Texture2D tex2 = mod.GetTexture("Items/Placeable/FourthAnniversaryNight");
            color = lightColor;
            alpha = (float)(Main.dayTime ? Main.time - 53000d : (32400d - Main.time));
            if (alpha < 0)
            {
                alpha = 0;
            }
            if (alpha > 1000)
            {
                alpha = 1000;
            }
            color.A = (byte)((int)(255f * (alpha / 1000f)));
            if (alpha > 0 && !(Main.bloodMoon && Main.time > 200 && Main.time < 31400) && !(Main.eclipse && Main.time > 200 && Main.time < 53000))
            {
                sb.Draw(tex2, new Vector2(item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
            }
            
            Texture2D tex3 = mod.GetTexture("Items/Placeable/FourthAnniversaryBloodMoon");
            color = lightColor;
            if (Main.bloodMoon)
            {
                alpha = (float)(Main.time <= 200d ? (Main.time * 5d) : (32400d - Main.time));
                if (alpha < 0)
                {
                    alpha = 0;
                }
                if (alpha > 1000)
                {
                    alpha = 1000;
                }
                color.A = (byte)((int)(255f * (alpha / 1000f)));
                if (alpha > 0)
                {
                    sb.Draw(tex3, new Vector2(item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}