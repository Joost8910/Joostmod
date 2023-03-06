using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class GnomeHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome Hat");
			Tooltip.SetDefault("'To show your loyalty to the Gnome God'");
			ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.vanity = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Silk, 3)
				.AddIngredient(ItemID.RedDye)
				.AddTile(TileID.WorkBenches)
				.Register();

		}
	}
}