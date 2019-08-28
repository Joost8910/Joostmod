using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class BubbleShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble Shield");
			Tooltip.SetDefault("Creates a bubble the knocks back enemies");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = 27000;
			item.rare = 2;
			item.accessory = true;
			item.damage = 10;
			item.summon = true;
			item.knockBack = 8f;
		}


		public override void UpdateAccessory(Player player, bool hideVisual)
		{
		player.GetModPlayer<JoostPlayer>(mod).bubbleShield = true;
		}


	}
}