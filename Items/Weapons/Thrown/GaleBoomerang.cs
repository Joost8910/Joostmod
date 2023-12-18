using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class GaleBoomerang : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gale Boomerang");
            Tooltip.SetDefault("A piercing boomerang that picks up enemies and items");
        }
        public override void SetDefaults()
        {
            Item.damage = 48;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 30;
            Item.height = 62;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7f;
            Item.value = 250000;
            Item.rare = ItemRarityID.LightRed;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>();
            Item.shootSpeed = 7f;
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
                .AddIngredient<Materials.TinyTwister>(50)
                .AddIngredient(ItemID.WoodenBoomerang)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

