using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Legendaries
{
    public class SkullStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone of Death");
            Tooltip.SetDefault("Grants the night owl buff while placed or in inventory\n" +
                "Found in the dungeon");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 50000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("SkullStone").Type;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 51 + (int)(Main.DiscoG * 0.3f), 76 + (int)(Main.DiscoG * 0.3f));
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.AddBuff(BuffID.NightOwl, 3);
        }
        public override void PostDrawInInventory(SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/SkullStone");
            drawColor = new Color(0, 51 + (int)(Main.DiscoG * 0.3f), 76 + (int)(Main.DiscoG * 0.3f));
            sb.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            Texture2D tex2 = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/SkullStoneEyes");
            Color color2 = new Color(0, Main.DiscoG, Main.DiscoG);
            sb.Draw(tex2, position, frame, color2, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch sb, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/SkullStone");
            float x = (float)(Item.width / 2f - tex.Width / 2f);
            float y = Item.height - tex.Height;
            lightColor = new Color(0, 51 + (int)(Main.DiscoG * 0.3f), 76 + (int)(Main.DiscoG * 0.3f));
            alphaColor = lightColor;
            sb.Draw(tex, new Vector2(Item.position.X - Main.screenPosition.X + tex.Width / 2 + x, Item.position.Y - Main.screenPosition.Y + tex.Height / 2 + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, SpriteEffects.None, 0f);
            Texture2D tex2 = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/SkullStoneEyes");
            Color color2 = new Color(0, Main.DiscoG, Main.DiscoG);
            sb.Draw(tex2, new Vector2(Item.position.X - Main.screenPosition.X + tex.Width / 2 + x, Item.position.Y - Main.screenPosition.Y + tex.Height / 2 + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color2, rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, SpriteEffects.None, 0f);
        }
    }
}
