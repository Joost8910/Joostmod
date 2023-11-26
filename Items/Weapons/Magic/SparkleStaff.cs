using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
{
    public class SparkleStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparkle Staff");
            Tooltip.SetDefault("'Pretty, oh so pretty'");
        }
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.width = 52;
            Item.height = 52;
            Item.mana = 24;
            Item.useTime = 7;
            Item.useAnimation = 28;
            Item.reuseDelay = 10;
            Item.staff[Item.type] = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 230000;
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Sparkle").Type;
            Item.shootSpeed = 2f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 1; i <= 10; i++)
            {
                float spread = 15f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                double baseAngle = Math.Atan2(velocity.X, velocity.Y);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
                velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * i, velocity.Y * i, type, damage, knockback, player.whoAmI);
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddIngredient(ItemID.PixieDust, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }

    }
}


