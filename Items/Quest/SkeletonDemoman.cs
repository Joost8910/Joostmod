using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class SkeletonDemoman : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Clogged Cannon");
		}

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 96;
			item.height = 46;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
