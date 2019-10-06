using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	[AutoloadEquip(EquipType.Shield)]
	public class XShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("X Shield");
			Tooltip.SetDefault("Creates X Parasites periodically\n" +
                "X Parasites infect the target, dealing damage over time and making them create more x parasites on death\n" + 
                "Red parasites make the target drop hearts and are scaled by ranged damage\n" + 
                "Green parasites make the target drop an energy orb and are scaled by throwing damage\n" + 
                "Blue parasites make the target drop mana stars and are scaled by magic damage\n" + 
                "Yellow parasites make the target drop more money and are scaled by melee damage\n" + 
                "Grants immunity to infection");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = 10000000;
			item.rare = -12;
			item.expert = true;
			item.accessory = true;
			item.defense = 10;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
		    player.GetModPlayer<JoostPlayer>(mod).XShield = true;
            player.buffImmune[mod.BuffType("InfectedRed")] = true;
            player.buffImmune[mod.BuffType("InfectedGreen")] = true;
            player.buffImmune[mod.BuffType("InfectedBlue")] = true;
            player.buffImmune[mod.BuffType("InfectedYellow")] = true;
        }
	}
}