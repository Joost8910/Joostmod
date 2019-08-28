using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class CactoidCommendation : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactoid Commendation");
			Tooltip.SetDefault("Non-corrupted Cactoids become friendly, fight for you, and regenerate health\n" + 
                "Other players on your team are granted this effect\n" +
                "Occasionally summons a cactoid");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = 30000;
			item.rare = 3;
            item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>(mod).cactoidCommendation = true;
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