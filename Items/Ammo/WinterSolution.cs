using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
			Item.shoot = Mod.Find<ModProjectile>("WinterSolution").Type - ProjectileID.PureSpray;
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
