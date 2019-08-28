using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class ICU : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Quad-retina Eye");
		}

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 36;
			item.height = 36;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
