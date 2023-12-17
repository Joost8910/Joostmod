using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
			Item.shoot = ModContent.ProjectileType<Projectiles.TemperateSolution>() - ProjectileID.PureSpray;
			Item.ammo = AmmoID.Solution;
			Item.width = 10;
			Item.height = 12;
			Item.value = Item.buyPrice(0, 0, 25, 0);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 999;
			Item.consumable = true;
		}

	}
}
