using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    public class CrimsonPommel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Pommel");
            Tooltip.SetDefault("Striking an enemy with a melee weapon inflicts Life Rend\n" +
                "Killing an enemy with Life Rend will heal you for 4% of the enemy's max life");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = 45000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostModPlayer>().crimsonPommel = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(230, 204, 128);
                }
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CorruptPommel>()
                .AddIngredient(ItemID.CrimtaneBar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}