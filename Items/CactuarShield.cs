using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	[AutoloadEquip(EquipType.Shield)]
	public class CactuarShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar Shield");
			Tooltip.SetDefault("Inflicts 20 times enemy contact damage back to the attacker");
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
			player.thorns += 20f;
		}
	}
}