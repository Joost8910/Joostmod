using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class DesertSolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desertification Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the Desert");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType("DesertSolution") - ProjectileID.PureSpray;
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
