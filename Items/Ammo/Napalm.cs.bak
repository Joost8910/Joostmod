using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
{
	public class Napalm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Napalm");
			Tooltip.SetDefault("'Fiery'");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 10;
			Item.width = 26;
			Item.height = 26;
			Item.consumable = true;
			Item.knockBack = 1f;
			Item.value = 120;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Napalm>();
			Item.ammo = Item.type;
		}
		public override void AddRecipes()
		{
			CreateRecipe(111)
				.AddIngredient<Materials.FireEssence>()
				.AddIngredient(ItemID.Bomb, 11)
				.AddTile<Tiles.ElementalForge>()
				.Register();
		}
	}
}

