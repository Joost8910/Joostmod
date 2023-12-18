using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Legendaries
{
    public class EvilStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone of Evil");
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
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("EvilStone").Type;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(127, 0, 255 - Main.DiscoG);
                }
            }
            TooltipLine line = new TooltipLine(Mod, "EvilStoneTooltip", "Grants the " + (WorldGen.crimson ? "wrath" : "rage") + " buff while placed or in inventory.\n"
                + "1/" + (Main.expertMode ? "25000" : "30000") + " chance to drop from enemies not spawned by statues");
            list.Add(line);
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().legendOwn = true;
            if (WorldGen.crimson)
            {
                player.AddBuff(BuffID.Wrath, 3);
            }
            else
            {
                player.AddBuff(BuffID.Rage, 3);
            }
        }
        public override void PostDrawInInventory(SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Legendaries/EvilStone");
            drawColor = new Color(127, 0, Main.DiscoG);
            sb.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            Texture2D tex2 = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Legendaries/EvilStonePupil");
            Color color2 = new Color(127, 0, 255 - Main.DiscoG);
            sb.Draw(tex2, position, frame, color2, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch sb, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Legendaries/EvilStone");
            float x = (float)(Item.width / 2f - tex.Width / 2f);
            float y = Item.height - tex.Height;
            lightColor = new Color(127, 0, Main.DiscoG);
            alphaColor = lightColor;
            sb.Draw(tex, new Vector2(Item.position.X - Main.screenPosition.X + tex.Width / 2 + x, Item.position.Y - Main.screenPosition.Y + tex.Height / 2 + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, SpriteEffects.None, 0f);
            Texture2D tex2 = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Items/Legendaries/EvilStonePupil");
            Color color2 = new Color(127, 0, 255 - Main.DiscoG);
            sb.Draw(tex2, new Vector2(Item.position.X - Main.screenPosition.X + tex.Width / 2 + x, Item.position.Y - Main.screenPosition.Y + tex.Height / 2 + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color2, rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, SpriteEffects.None, 0f);
        }
    }
}

