using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Boomerain : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boomerain");
			Tooltip.SetDefault("A boomerang that drops damaging rain below it");
		}
		public override void SetDefaults()
		{
			item.damage = 31;
			item.thrown = true;
			item.width = 18;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 50000;
			item.rare = 4;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Boomerain");
			item.shootSpeed = 14f;
		}
		public override bool CanUseItem(Player player)
        {
 			if (player.ownedProjectileCounts[item.shoot] >= item.stack) 
	        {
                return false;
            }
            else return true;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "WaterEssence", 50);
			recipe.AddIngredient(ItemID.WoodenBoomerang);
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

