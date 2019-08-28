using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class FloweringCactoid : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactoid Flower");
		}

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 18;
			item.height = 16;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
