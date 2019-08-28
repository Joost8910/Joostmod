using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class CactusGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Glove");
            Tooltip.SetDefault("Throws a cactus that explodes into thorns");
        }
        public override void SetDefaults()
        {
            item.damage = 18;
            item.thrown = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.knockBack = 4;
            item.value = 60000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ThornyCactus");
            item.shootSpeed = 11f;
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


