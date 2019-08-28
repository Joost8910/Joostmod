using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Shoes)]
    public class CactusBoots : ModItem
    { 
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Boots");
            Tooltip.SetDefault("Sprouts damaging cacti as you walk");
        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 28;
            item.value = 75000;
            item.rare = 3;
            item.accessory = true;
            item.damage = 18;
            item.summon = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>(mod).cactusBoots = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(230, 204, 128);
                }
            }
        }
    }
}