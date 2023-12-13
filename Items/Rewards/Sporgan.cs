using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
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
			Item.width = 26;
			Item.height = 26;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
			Item.damage = 12;
            Item.knockBack = 0.1f;
            Item.DamageType = DamageClass.Summon;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().sporganItem = Item;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "ItemName")
                    {
                        line2.OverrideColor = new Color(230, 204, 128);
                    }
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }
    }
}