using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class CactuarMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar Mask");
			ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 26;
			Item.vanity = true;
			Item.value = 5000;
			Item.rare = ItemRarityID.Green;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Cactus, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}