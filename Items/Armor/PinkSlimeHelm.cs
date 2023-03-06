using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class PinkSlimeHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pink Slime Helm");
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = Item.buyPrice(0, 7, 50, 0);
			Item.rare = ItemRarityID.Pink;
			Item.defense = 6;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("PinkSlimeCoat").Type && legs.type == Mod.Find<ModItem>("PinkSlimeLeggings").Type;
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Press the Armor Ability key to become slimed\n" +
				"While slimed, you take half damage and bounce off surfaces and enemies, but cannot use items";
			player.GetModPlayer<JoostPlayer>().pinkSlimeArmor = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PinkSlimeBlock, 20)
				.AddTile(TileID.Solidifier)
				.Register();
			CreateRecipe()
				.AddIngredient(ItemID.PinkGel, 20)
				.AddTile(TileID.Solidifier)
				.Register();
		}
	}
}