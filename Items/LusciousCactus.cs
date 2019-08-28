using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class LusciousCactus : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luscious Cactus");
			Tooltip.SetDefault("'Choose your loot!'");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 22;
			item.value = 6000;
			item.rare = 3;
            item.bait = 60;
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

