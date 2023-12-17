using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Thrown;

namespace JoostMod.Items.Weapons.Thrown
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
            Item.damage = 18;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.value = 60000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ThornyCactus>();
            Item.shootSpeed = 11f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.LusciousCactus>(10)
                .Register();
        }
    }
}


