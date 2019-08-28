using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class DesertCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Core");
			Tooltip.SetDefault("'Filled with mysterious energy'");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 26;
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = 1;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 0;
			item.value = 100000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
		}

	}
}

