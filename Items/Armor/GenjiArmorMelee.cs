using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class GenjiArmorMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Genji Armor");
            Tooltip.SetDefault("Enemies are most likely to target you\n" + "Max Life increased by 250");
        }
        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 50;
            item.value = 10000000;
            item.rare = 11;
            item.defense = 35;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 255, 0);
                }
            }
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 250;
            player.aggro += 300;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GenjiToken", 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}