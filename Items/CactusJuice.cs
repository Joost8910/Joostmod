using Terraria.ID; 
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class CactusJuice : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Juice");
            Tooltip.SetDefault("'It'll quench ya'\n" +
            "'Nothings quenchier'\n" +
            "'It's the quenchiest!");
        }
        public override void SetDefaults()
        {
            item.maxStack = 30;
            item.consumable = true;
            item.width = 20;
            item.height = 26;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 2;
            item.knockBack = 5;
            item.value = 750;
            item.rare = 2;
            item.UseSound = SoundID.Item3;
            item.buffTime = 7200;
            item.buffType = mod.BuffType("CactusJuice");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SucculentCactus");
            recipe.AddIngredient(ItemID.Bottle, 2);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }

    }
}

