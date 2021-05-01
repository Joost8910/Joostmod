using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	[AutoloadEquip(EquipType.Wings)]
	public class RocWings : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Roc Wings");
            Tooltip.SetDefault("Allows short flight and gliding\n" + "Hold UP while not flying or using an item to glide\n" + "Hold left or right to angle your glide");
        }

		public override void SetDefaults()
		{
			item.width = 46;
			item.height = 32;
			item.value = 60000;
			item.rare = 3;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 30;
            player.GetModPlayer<JoostPlayer>().rocWings = true;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.5f;
			ascentWhenRising = 0.1f;
			maxCanAscendMultiplier = 0.5f;
			maxAscentMultiplier = 1.4f;
			constantAscend = 0.1f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 6.25f;
			acceleration *= 1.25f;
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