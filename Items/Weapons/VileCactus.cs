using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class VileCactus : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vile Cactus Worm");
			Tooltip.SetDefault("Casts a controllable cactus worm");
		}
		public override void SetDefaults()
		{
			item.damage = 21;
			item.magic = true;
            item.mana = 14;
			item.width = 50;
			item.height = 50;
			item.useTime = 28;
			item.useAnimation = 28;
            item.reuseDelay = 5;
			item.useStyle = 5;
            Item.staff[item.type] = true;
            item.channel = true;
			item.noMelee = true; 
			item.knockBack = 2;
			item.value = 60000;
			item.rare = 2;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("VileCactusWorm");
			item.shootSpeed = 10f;
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


