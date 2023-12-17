using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Magic;

namespace JoostMod.Items.Weapons.Magic
{
    public class EvenMoreNeedles : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Needle Wrath");
            Tooltip.SetDefault("Unleashes a Hurricane of needles");
        }
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 20;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 2;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Needle3>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 15f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
            velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
            velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
            if (player.itemAnimation % 4 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item7, position);
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.Cactustoken>()
                .Register();
        }
    }
}

