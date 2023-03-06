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
			Item.width = 26;
			Item.height = 26;
			Item.value = 30000;
			Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().cactoidCommendation = true;
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