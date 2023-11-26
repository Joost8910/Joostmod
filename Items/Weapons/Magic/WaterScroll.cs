using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
{
    public class WaterScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scroll of Water");
            Tooltip.SetDefault("Creates a controllable ball of water\n" + "Collects nearby water to grow");
        }
        public override void SetDefaults()
        {
            Item.damage = 36;
            Item.DamageType = DamageClass.Magic;
            Item.width = 36;
            Item.height = 36;
            Item.mana = 20;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 2;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.knockBack = 3;
            Item.UseSound = SoundID.Item21;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WaterBall").Type;
            Item.shootSpeed = 15f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }

    }
}

