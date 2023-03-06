using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    public class CorruptPommel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Pommel");
            Tooltip.SetDefault("Striking an enemy with a melee weapon inflicts Corrupted Soul, dealing damage over time\n" +
                               "Enemies that die with Corrupted Soul damages a nearby enemy for 25% \n" +
                               "of the corrupted enemy's max life (capping at 9999) and inflicts Corrupted Soul\n");
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
            player.GetModPlayer<JoostModPlayer>().corruptPommel = true;
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
            //TODO: Replace this recipe with use of shimmer instead
            CreateRecipe()
                .AddIngredient<CrimsonPommel>()
                .AddIngredient(ItemID.DemoniteBar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}