using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class Tornade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tornade");
            Tooltip.SetDefault("Releases a miniature tornado on impact");
        }
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Throwing;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 22;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.value = 500;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.Tornade>();
            Item.shootSpeed = 10f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.Grenade, 50)
                .AddIngredient<Materials.AirEssence>()
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }

    }
}

