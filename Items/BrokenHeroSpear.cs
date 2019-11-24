using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class BrokenHeroSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Hero Spear");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 58;
			item.height = 38;
			item.value = 100000;
			item.rare = 8;
		}

	}
}

