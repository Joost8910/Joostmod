using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
    public class ShieldSapling : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling - Shield");
            Tooltip.SetDefault("Tries to block attackers behind you\n" + 
                "Cannot block projectiles that have a base damage greater than 30");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.value = 20000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            //item.defense = 4;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().shieldSaplingItem = Item;
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
                .AddRecipeGroup("JoostMod:Saplings")
                .AddIngredient(ItemID.CopperBar, 7)
                .Register();
            CreateRecipe()
                .AddRecipeGroup("JoostMod:Saplings")
                .AddIngredient(ItemID.TinBar, 7)
                .Register();
        }
    }
}