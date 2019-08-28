using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class StoneofJordan : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone of Jordan");
            Tooltip.SetDefault("10% increased damage\n" + "Max Life and Mana increased by 20");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 36;
            item.value = 100000;
            item.rare = 3;
            item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 128, 0);
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.1f;
            player.statLifeMax2 += 20;
            player.statManaMax2 += 20;
        }
    }
}