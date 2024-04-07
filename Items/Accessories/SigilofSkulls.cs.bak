using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class SigilofSkulls : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sigil of Skulls");
			Tooltip.SetDefault("Summon multiple skulls when below half health\n" + 
			"Increases max number of minions");
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.rare = ItemRarityID.Yellow;
			Item.value = 200000;
			Item.accessory = true;
			Item.damage = 125;
			Item.DamageType = DamageClass.Summon;
			Item.knockBack = 5.5f;
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
			player.maxMinions++;
			player.GetModPlayer<JoostPlayer>().SkullSigilItem = Item;
		}
	}
}