using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class GilgameshMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh Mask");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 30;
			Item.vanity = true;
			Item.value = 100000;
			Item.rare = ItemRarityID.Red;
		}

	}
}