using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
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
            Item.damage = 45;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.reuseDelay = 8;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 5;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.SandstormJavelin>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.DesertCore>()
                .AddRecipeGroup("JoostMod:AnyAdamantite", 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


