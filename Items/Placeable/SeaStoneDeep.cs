using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
    public class SeaStoneDeep : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone of the Deep Sea");
            Tooltip.SetDefault("Grants crate buff while placed or in inventory\n" + 
                "Fished in caverns");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 32;
            item.height = 28;
            item.useTime = 10;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 50000;
            item.rare = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("SeaStoneDeep");
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 0, (51 + (int)((float)(255 - Main.DiscoB) * 0.8f)));
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().deepStone = true;
            player.AddBuff(BuffID.Crate, 3);
        }
        public override void PostDrawInInventory(SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.GetTexture("JoostMod/Items/Placeable/SeaStoneDeep");
            drawColor = new Color(0, 0, (51 + (int)((float)(255 - Main.DiscoB) * 0.8f)));
            sb.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch sb, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = ModContent.GetTexture("JoostMod/Items/Placeable/SeaStoneDeep");
            float x = (float)(item.width / 2f - tex.Width / 2f);
            float y = (float)(item.height - tex.Height);
            lightColor = new Color(0, 0, (51 + (int)((float)(255 - Main.DiscoB) * 0.8f)));
            alphaColor = lightColor;
            sb.Draw(tex, new Vector2(item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
        }
    }
}

