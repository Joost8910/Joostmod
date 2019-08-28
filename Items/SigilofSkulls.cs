using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class SigilofSkulls : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sigil of Skulls");
			Tooltip.SetDefault("Summon multiple skulls when below half health\n" + 
			"Increases max number of minions");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.rare = 8;
			item.value = 200000;
			item.accessory = true;
			item.damage = 125;
			item.summon = true;
			item.knockBack = 5.5f;
		}


		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions++;
			player.GetModPlayer<JoostPlayer>(mod).SkullSigil = true;
		}
	}
}