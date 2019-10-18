using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class BubbleShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble Shield");
			Tooltip.SetDefault("Creates a bubble that knocks back enemies");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = 27000;
			item.rare = 2;
			item.accessory = true;
			item.damage = 10;
			item.summon = true;
			item.knockBack = 8f;
		}


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria")
                {
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.overrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().bubbleShield = true;
        }


	}
}