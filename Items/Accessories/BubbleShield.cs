using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class BubbleShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble Shield");
			Tooltip.SetDefault("Creates a bubble that knocks back enemies\n" +
                "Fished in the ocean");
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.value = 27000;
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;
			Item.damage = 10;
			Item.DamageType = DamageClass.Summon;
			Item.knockBack = 8f;
		}


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().bubbleShieldItem = Item;
        }
	}
}