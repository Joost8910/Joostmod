using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CactusCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Cannon");
			Tooltip.SetDefault("Fires a sticky cactus that damages enemies multiple times");
		}
		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 50;
			item.height = 36;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 0;
			item.value = 60000;
			item.rare = 2;
			item.UseSound = SoundID.Item61;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("StickyCactus");
			item.shootSpeed = 14f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, -5);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LusciousCactus", 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


