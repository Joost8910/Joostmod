using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Materials
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
			Item.maxStack = 999;
			Item.width = 26;
			Item.height = 22;
			Item.value = 6000;
			Item.rare = ItemRarityID.Orange;
            Item.bait = 60;
            Item.UseSound = SoundID.Item1;
		}
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(106, 63, 202);
                }
			}
		}
	}
}

