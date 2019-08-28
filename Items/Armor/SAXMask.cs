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
			item.width = 20;
			item.height = 18;
			item.vanity = true;
			item.value = 100000;
			item.rare = 9;
		}

	}
}