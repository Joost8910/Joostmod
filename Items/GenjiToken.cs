using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class GenjiToken : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genji Token");
            Tooltip.SetDefault("'Choose your loot!'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 26;
            item.height = 26;
            item.useTime = 2;
            item.useAnimation = 2;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 10000000;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(106, 63, 202);
                }
            }
        }
    }
}

