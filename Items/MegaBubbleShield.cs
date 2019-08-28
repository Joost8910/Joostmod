using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class MegaBubbleShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mega Bubble Shield");
			Tooltip.SetDefault("Creates a powerful bubble the knocks back enemies");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = 270000;
			item.rare = 8;
			item.accessory = true;
			item.damage = 90;
			item.summon = true;
			item.knockBack = 18.5f;
		}


		public override void UpdateAccessory(Player player, bool hideVisual)
		{
		player.GetModPlayer<JoostPlayer>(mod).megaBubbleShield = true;
		}


	}
}