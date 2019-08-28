using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class WaterEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Essence");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 32;
			item.height = 32;
			item.value = 10000;
			item.rare = 4;
		}

	}
}

