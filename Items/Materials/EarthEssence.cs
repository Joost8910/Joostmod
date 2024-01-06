using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Materials
{
	public class EarthEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earth Essence");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 7));
			ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.width = 26;
			Item.height = 26;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
		}

	}
}

