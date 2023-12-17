using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Summon;

namespace JoostMod.Items.Weapons.Summon
{
    public class WindWheel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hurricane Windwheel");
            Tooltip.SetDefault("Creates a swirling current of wind that damages enemies");
        }
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 12;
            Item.width = 54;
            Item.height = 56;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0f;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Swirlwind>();
            Item.shootSpeed = 1f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.TinyTwister>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

