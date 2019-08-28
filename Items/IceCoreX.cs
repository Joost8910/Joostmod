using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class IceCoreX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Core-X");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 64;
            item.height = 64;
			item.value = 10000000;
            item.rare = 11;

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

