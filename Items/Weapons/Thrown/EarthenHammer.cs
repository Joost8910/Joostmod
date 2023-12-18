using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Thrown;

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
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.EarthenHammer>();
            Item.shootSpeed = 8f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave1>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave2>()] >= 1)
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
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

