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
			item.damage = 48;
			item.thrown = true;
			item.width = 30;
			item.height = 62;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 7f;
			item.value = 50000;
			item.rare = 4;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("GaleBoomerang");
			item.shootSpeed = 7f;
		}
        public override bool CanUseItem(Player player)
        {
            if ((player.ownedProjectileCounts[item.shoot]) >= item.stack)
            {
                return false;
            }
            else return true;
        }
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TinyTwister", 50);
			recipe.AddIngredient(ItemID.WoodenBoomerang);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 3);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 3);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 3);
            recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

