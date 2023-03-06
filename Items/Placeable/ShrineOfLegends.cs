using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
    public class ShrineOfLegends : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shrine of Legends");
            Tooltip.SetDefault("Used to craft legendary weapons");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 26;
            Item.height = 26;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 500000;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("ShrineOfLegends").Type;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.5f)));
                }
            }
        }
        public override void PostDrawInInventory(SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/ShrineOfLegendsGem");
            drawColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.5f)));
            sb.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch sb, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/ShrineOfLegendsGem");
            float x = (float)(Item.width / 2f - tex.Width / 2f);
            float y = (float)(Item.height - tex.Height);
            lightColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.5f)));
            alphaColor = lightColor;
            sb.Draw(tex, new Vector2(Item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, Item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
        }
    }
}

