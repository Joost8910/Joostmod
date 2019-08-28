using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class Skeletron : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Big Bone");
		}

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 50;
			item.height = 32;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
