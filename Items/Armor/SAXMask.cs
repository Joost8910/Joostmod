using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SAXMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SA-X Mask");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 18;
			Item.vanity = true;
			Item.value = 100000;
			Item.rare = ItemRarityID.Cyan;
		}

	}
}