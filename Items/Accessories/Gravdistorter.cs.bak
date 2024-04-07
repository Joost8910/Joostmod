using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class Gravdistorter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gravity Distortion Core");
			Tooltip.SetDefault("Gravity gets weird");
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(BuffID.VortexDebuff, 2);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.InletPump, 2)
				.AddIngredient(ItemID.OutletPump, 2)
				.AddIngredient(ItemID.GravitationPotion, 1)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}

