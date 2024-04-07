using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class LunarShuriken : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Shuriken");
            Tooltip.SetDefault("'Unleash the power of the moon'");
        }
        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Throwing;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 5;
            Item.value = 20000;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.LunarShuriken>();
            Item.shootSpeed = 23f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(333)
                .AddIngredient(ItemID.LunarBar)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

    }
}

