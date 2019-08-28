using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Yonade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yonade");
			Tooltip.SetDefault("Drops and explodes on a critical hit");
		}
		public override void SetDefaults()
		{
			item.damage = 23;
			item.melee = true;
			item.width = 30;
			item.height = 30;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.knockBack = 4.2f;
			item.channel = true;
            item.value = 80000;
            item.rare = 3;
            item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("Yonade");
			item.shootSpeed = 10f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Valor);
            recipe.AddIngredient(ItemID.Grenade, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

