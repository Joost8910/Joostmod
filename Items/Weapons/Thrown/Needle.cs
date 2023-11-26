using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class Needle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Needle");
            Tooltip.SetDefault("'Better than a sharp stick in the eye.'\n" + "'Oh wait...'");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Throwing;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 2;
            Item.height = 12;
            Item.useTime = 4;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 0;
            Item.value = 0;
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Needle").Type;
            Item.shootSpeed = 8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
            velocity.X = perturbedSpeed.X;
            velocity.Y = perturbedSpeed.Y;
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe(60)
                .AddIngredient(ItemID.Cactus)
                .Register();
        }
    }
}
