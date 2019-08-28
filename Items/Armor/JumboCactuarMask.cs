using System.Collections.Generic;
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
			item.width = 48;
			item.height = 48;
			item.vanity = true;
			item.value = 100000;
			item.rare = 7;
		}

	}
}