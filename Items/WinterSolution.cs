using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class WinterSolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Winter Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the tundra\n" + 
				"'Winter is coming'");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType("WinterSolution") - ProjectileID.PureSpray;
			item.ammo = AmmoID.Solution;
			item.width = 10;
			item.height = 12;
			item.value = Item.buyPrice(0, 0, 25, 0);
			item.rare = 3;
			item.maxStack = 999;
			item.consumable = true;
		}

	}
}
