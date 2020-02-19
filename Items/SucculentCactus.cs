using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class SucculentCactus : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Succulent Cactus");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 1000;
			item.rare = 2;
			item.bait = 20;
		}
	}
}

