using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Summon;

namespace JoostMod.Items.Weapons.Summon
{
    public class WindChimes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chimes of the Wind");
            Tooltip.SetDefault("Creates currents of wind that damages enemies");
        }
        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 7f;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item35;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Wind>();
            Item.shootSpeed = 12.8f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 30);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity.X = (8f + Main.rand.NextFloat() * 8f) * player.direction + (player.direction * player.velocity.X > 0 ? player.velocity.X : 0);
            velocity.Y = 0;
            position.X -= 1000 * player.direction;
            position.Y += Main.rand.Next(-8, 8) * 10;
            knockback = Math.Abs(velocity.X);
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bell)
                .AddIngredient<Materials.TinyTwister>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

