using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	[AutoloadEquip(EquipType.Shield)]
	public class FleshShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shield of Flesh");
			Tooltip.SetDefault("Double tap left or right to dash into enemies\n" + 
                "Occasionally summons leeches that steal life\n" + 
                "Leeches summon faster the less life you have");
		}
		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 52;
			item.value = 85000;
			item.rare = 3;
			item.accessory = true;
			item.defense = 2;
            item.damage = 40;
            item.melee = true;
            item.knockBack = 9;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>(mod).fleshShield = true;
            player.GetModPlayer<JoostPlayer>(mod).dashType = 1;
            player.GetModPlayer<JoostPlayer>(mod).dashDamage = (int)(40 * (player.allDamage + player.meleeDamage - 1) * player.meleeDamageMult * player.allDamageMult);
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