using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using JoostMod.Items.Materials;

namespace JoostMod.Items.Weapons.Thrown
{
    public class Fireball : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
            Tooltip.SetDefault("Explodes into lingering flames");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
        }
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Throwing;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 22;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 1;
            Item.value = 500;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.Fireball>();
            Item.shootSpeed = 7.5f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.Gel, 10)
                .AddIngredient<FireEssence>()
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }

    }
}

