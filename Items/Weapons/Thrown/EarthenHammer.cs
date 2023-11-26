using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class EarthenHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthen Hammer");
            Tooltip.SetDefault("A mighty hammer that creates a shockwave on impact with the ground");
        }
        public override void SetDefaults()
        {
            Item.damage = 69;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 13;
            Item.value = 225000;
            Item.rare = ItemRarityID.LightRed;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("EarthenHammer").Type;
            Item.shootSpeed = 8f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave1").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave2").Type] >= 1)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 75)
                .AddRecipeGroup("JoostMod:AnyCobalt", 3)
                .AddRecipeGroup("JoostMod:AnyMythril", 3)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

