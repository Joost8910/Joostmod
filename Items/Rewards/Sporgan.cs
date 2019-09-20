using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class Sporgan : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sporgan");
			Tooltip.SetDefault("Spews spore clouds after taking damage");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = 50000;
			item.rare = 3;
            item.accessory = true;
			item.damage = 10;
			item.summon = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>(mod).sporgan = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria")
                {
                    if (line2.Name == "ItemName")
                    {
                        line2.overrideColor = new Color(230, 204, 128);
                    }
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.overrideColor = Color.DarkGray;
                    }
                }
            }
        }
    }
}