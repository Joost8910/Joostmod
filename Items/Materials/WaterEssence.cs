using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Materials
{
	public class WaterEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Essence");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
			ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.width = 32;
			Item.height = 32;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
        }
        public override bool CanStackInWorld(Item item2)
        {
            return false;
        }

    }
}

