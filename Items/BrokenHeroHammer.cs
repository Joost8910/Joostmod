using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class BrokenHeroHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Hero Hammer");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 40;
			item.height = 36;
			item.value = 100000;
			item.rare = 8;
		}

	}
}

