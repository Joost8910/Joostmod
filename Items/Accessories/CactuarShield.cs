using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class CactuarShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar Shield");
			Tooltip.SetDefault("Inflicts 20 times enemy contact damage back to the attacker\n" + 
                "Grants immunity to knockback");
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
			player.thorns += 20f;
            player.noKnockback = true;
		}
	}
}