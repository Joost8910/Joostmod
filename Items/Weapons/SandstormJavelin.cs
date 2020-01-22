using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class SandstormJavelin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Javelin");
            Tooltip.SetDefault("Hold attack to charge the throw\nMax charge drills through sand and launches it backwards");
        }
        public override void SetDefaults()
        {
            item.damage = 48;
            item.thrown = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 24;
            item.useAnimation = 24;
            item.reuseDelay = 8;
            item.channel = true;
            item.useStyle = 1;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.knockBack = 5;
            item.value = 225000;
            item.rare = 5;
            item.UseSound = SoundID.Item7;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SandstormJavelin");
            item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


