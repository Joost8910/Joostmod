using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class CactuarPants : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar Pants");
			ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;

        }
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.vanity = true;
			Item.value = 7500;
			Item.rare = ItemRarityID.Green;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Cactus, 15)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}