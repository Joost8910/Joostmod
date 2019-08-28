using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class TemperateSolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Temperate Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nRemoves the Desert and Tundra");
		}

		public override void SetDefaults()
		{
			item.shoot = mod.ProjectileType("TemperateSolution") - ProjectileID.PureSpray;
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
