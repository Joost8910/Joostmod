using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
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
                "Grants immunity to knockback and infection");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = 10000000;
			Item.rare = ItemRarityID.Expert;
			Item.expert = true;
			Item.accessory = true;
			Item.defense = 10;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
		    player.GetModPlayer<JoostPlayer>().XShieldItem = Item;
            player.buffImmune[ModContent.BuffType<Buffs.InfectedRed>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.InfectedGreen>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.InfectedBlue>()] = true;
            player.buffImmune[ModContent.BuffType<Buffs.InfectedYellow>()] = true;
            player.noKnockback = true;
        }
	}
}