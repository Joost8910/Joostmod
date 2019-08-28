using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class BrokenHeroFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Hero Flail");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 26;
			item.value = 1000000;
			item.rare = 8;
		}

	}
}

