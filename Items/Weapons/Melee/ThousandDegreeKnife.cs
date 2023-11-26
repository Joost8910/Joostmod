using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class ThousandDegreeKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("1000'C Degrees Knife");
            Tooltip.SetDefault("'That's 1832 degrees Fahrenheit");
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 20;
            Item.height = 18;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = 500000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LunarBar, 8)
                .AddIngredient<Materials.FireEssence>(30)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

