using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
{
    public class WaterWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Tendril");
            Tooltip.SetDefault("Left click for a slapping tendril\n" +
                "Damage dealt is based on the tendril's speed\n" +
                "Right click for a grasping tendril\n" +
                "Grabs hit enemies and items");
        }
        public override void SetDefaults()
        {
            Item.damage = 66;
            Item.DamageType = DamageClass.Magic;
            Item.width = 36;
            Item.height = 36;
            Item.mana = 12;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.reuseDelay = 2;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.knockBack = 8;
            Item.UseSound = SoundID.Item21;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.WaterWhip>();
            Item.shootSpeed = 5f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.UseSound = SoundID.Item21;
            }
            else
            {
                Item.UseSound = SoundID.Item21;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<Projectiles.Magic.WaterWhip2>();
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, -1);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();

        }

    }
}


