using Terraria;
using Terraria.ID; 
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
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
            Item.maxStack = 30;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 26;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 2;
            Item.knockBack = 5;
            Item.value = 750;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item3;
            Item.buffTime = 7200;
            Item.buffType = ModContent.BuffType<Buffs.CactusJuice>();
        }
        public override void AddRecipes()
        {
            CreateRecipe(2)
                .AddIngredient<Materials.SucculentCactus>()
                .AddIngredient(ItemID.Bottle, 2)
                .Register();
        }

    }
}

