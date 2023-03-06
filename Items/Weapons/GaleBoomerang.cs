using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class GaleBoomerang : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gale Boomerang");
			Tooltip.SetDefault("A piercing boomerang that picks up enemies and items");
		}
		public override void SetDefaults()
		{
			Item.damage = 48;
			Item.DamageType = DamageClass.Throwing;
			Item.width = 30;
			Item.height = 62;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7f;
			Item.value = 250000;
			Item.rare = ItemRarityID.LightRed;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("GaleBoomerang").Type;
			Item.shootSpeed = 7f;
		}
		public override bool CanUseItem(Player player)
		{
			if ((player.ownedProjectileCounts[Item.shoot]) >= Item.stack)
			{
				return false;
			}
			else return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.TinyTwister>(50)
				.AddIngredient(ItemID.WoodenBoomerang)
				.AddRecipeGroup("JoostMod:AnyCobalt", 3)
				.AddRecipeGroup("JoostMod:AnyMythril", 3)
				.AddRecipeGroup("JoostMod:AnyAdamantite", 3)
				.AddTile<Tiles.ElementalForge>()
				.Register();
		}
	}
}

