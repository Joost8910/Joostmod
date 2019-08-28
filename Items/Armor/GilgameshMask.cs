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
			item.width = 28;
			item.height = 30;
			item.vanity = true;
			item.value = 100000;
			item.rare = 9;
		}

	}
}