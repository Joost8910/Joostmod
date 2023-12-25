using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
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
            Item.width = 50;
            Item.height = 34;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.createTile = ModContent.TileType<Tiles.FourthAnniversary>();
            Item.placeStyle = 0;
        }
        public override void PostDrawInInventory(SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Placeable/FourthAnniversarySolarEclipse");
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

            Texture2D tex2 = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Placeable/FourthAnniversaryNight");
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

            Texture2D tex3 = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Placeable/FourthAnniversaryBloodMoon");
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
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Placeable/FourthAnniversarySolarEclipse");
            float x = (float)(Item.width / 2f - tex.Width / 2f);
            float y = (float)(Item.height - tex.Height);
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
                    sb.Draw(tex, new Vector2(Item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, Item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }

            Texture2D tex2 = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Placeable/FourthAnniversaryNight");
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
                sb.Draw(tex2, new Vector2(Item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, Item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
            }
            
            Texture2D tex3 = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Placeable/FourthAnniversaryBloodMoon");
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
                    sb.Draw(tex3, new Vector2(Item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, Item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}