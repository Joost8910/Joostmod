using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class CactuarCoat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar Coat");
			ArmorIDs.Body.Sets.HidesTopSkin[Item.bodySlot] = true;

        }
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.vanity = true;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Cactus, 20)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}