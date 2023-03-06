using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class ChlorophyteGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Glove");
            Tooltip.SetDefault("Throws spore clouds");
        }
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = 180000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shoot = ProjectileID.SporeCloud;
            Item.shootSpeed = 4f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 35f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            for (int i = 1; i < 5; i++)
            {
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                velocity.X = i * baseSpeed * (float)Math.Sin(randomAngle);
                velocity.Y = i * baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                velocity.X = i * baseSpeed * (float)Math.Sin(randomAngle);
                velocity.Y = i * baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


