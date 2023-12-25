using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class MegaBubbleShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mega Bubble Shield");
			Tooltip.SetDefault("Creates a powerful bubble that knocks back enemies");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = 270000;
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
			Item.damage = 90;
			Item.DamageType = DamageClass.Summon;
			Item.knockBack = 18.5f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
		    player.GetModPlayer<JoostPlayer>().megaBubbleShieldItem = Item;
		}
	}
}