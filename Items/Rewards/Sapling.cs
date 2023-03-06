using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class Sapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling");
			Tooltip.SetDefault("Can be upgraded with items to assist you\n" + 
			"Rides around on your back");
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 30;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
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
    }
}