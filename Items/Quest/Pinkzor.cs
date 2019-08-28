using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class Pinkzor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Congealed Pink Gel");
		}

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 32;
			item.height = 30;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
