using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class StormyCollar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flower Collar");
			Tooltip.SetDefault("In loving memory of Stormy");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.shoot = mod.ProjectileType("Stormy");
			item.buffType = mod.BuffType("Stormy");
            item.rare = 2;
            item.value = 0;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(107, 173, 120);
                }
                if (line.mod == "Terraria" && line.Name == "Price")
                {
                    line.text = "Priceless";
                }
            }
        }
        public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}