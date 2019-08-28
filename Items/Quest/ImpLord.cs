using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
	public class ImpLord : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thick Imp Tail");
		}

		public override void SetDefaults()
		{
			item.questItem = true;
			item.maxStack = 1;
			item.width = 20;
			item.height = 20;
			item.uniqueStack = true;
			item.rare = -11;		
		}
	}
}
