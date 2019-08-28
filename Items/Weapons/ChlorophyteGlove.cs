using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            item.damage = 45;
            item.thrown = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 40000;
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.shoot = 228;
            item.shootSpeed = 4f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 35f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double baseAngle = Math.Atan2(speedX, speedY);
            for (int i = 1; i < 5; i++)
            {
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                speedX = i * baseSpeed * (float)Math.Sin(randomAngle);
                speedY = i * baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                speedX = i * baseSpeed * (float)Math.Sin(randomAngle);
                speedY = i * baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


