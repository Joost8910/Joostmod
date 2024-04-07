using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class MooshroomLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mooshroom Bottom");
			ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;

		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.vanity = true;
			Item.value = 7500;
			Item.rare = ItemRarityID.LightRed;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Mushroom, 15)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}