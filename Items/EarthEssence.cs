using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class EarthEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earth Essence");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 7));
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 26;
			item.value = 10000;
			item.rare = 4;
		}

	}
}

