using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Legendaries
{
    public class InfernoStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone of Fire");
            Tooltip.SetDefault("Grants warmth buff while placed or in inventory\n" +
                "Found in hell");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 50000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("InfernoStone").Type;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, (int)(Main.DiscoG * 0.7f), 0);
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.AddBuff(BuffID.Warmth, 3);
        }
        public override void PostDrawInInventory(SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/InfernoStone");
            drawColor = new Color(255, (int)(Main.DiscoG * 0.7f), 0);
            sb.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch sb, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Placeable/InfernoStone");
            float x = (float)(Item.width / 2f - tex.Width / 2f);
            float y = Item.height - tex.Height;
            lightColor = new Color(255, (int)(Main.DiscoG * 0.7f), 0);
            alphaColor = lightColor;
            sb.Draw(tex, new Vector2(Item.position.X - Main.screenPosition.X + tex.Width / 2 + x, Item.position.Y - Main.screenPosition.Y + tex.Height / 2 + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, SpriteEffects.None, 0f);
        }
    }
}
