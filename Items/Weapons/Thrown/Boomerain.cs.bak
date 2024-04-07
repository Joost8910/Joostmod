using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class Boomerain : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerain");
            Tooltip.SetDefault("A boomerang that drops damaging rain below it");
        }
        public override void SetDefaults()
        {
            Item.damage = 31;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 18;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = 225000;
            Item.rare = ItemRarityID.LightRed;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.Boomerain>();
            Item.shootSpeed = 14f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] >= Item.stack)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddIngredient(ItemID.WoodenBoomerang)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

