using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class FireEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Essence");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
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

