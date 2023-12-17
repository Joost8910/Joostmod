using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
			Item.shoot = ModContent.ProjectileType<Projectiles.DesertSolution>() - ProjectileID.PureSpray;
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
