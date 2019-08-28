using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class TinyTwister : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Air Essence");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(3, 6));
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 22;
			item.height = 28;
			item.value = 10000;
			item.rare = 4;
		}

	}
}

