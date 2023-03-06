using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class SpaceJump : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Space Jump");
			Tooltip.SetDefault("Allows you to jump infinitely");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = 52000;
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 8)
				.AddIngredient(ItemID.MeteoriteBar, 8)
				.AddIngredient(ItemID.BundleofBalloons)
				.AddTile(TileID.MythrilAnvil)
				.Register();

		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<JoostPlayer>().spaceJump = true;
		}

	}
}