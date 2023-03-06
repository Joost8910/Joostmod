using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class JumboCactuarMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jumbo Cactuar Mask");
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.vanity = true;
			Item.value = 100000;
			Item.rare = ItemRarityID.Lime;
		}

	}
}