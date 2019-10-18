using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
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
            item.damage = 60;
            item.magic = true;
            item.mana = 20;
            item.width = 28;
            item.height = 30;
            item.useTime = 2;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 10000000;
            item.rare = 11;
            item.UseSound = SoundID.Item7;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Needle3");
            item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 15f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double baseAngle = Math.Atan2(speedX, speedY);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            speedX = baseSpeed * (float)Math.Sin(randomAngle);
            speedY = baseSpeed * (float)Math.Cos(randomAngle);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            speedX = baseSpeed * (float)Math.Sin(randomAngle);
            speedY = baseSpeed * (float)Math.Cos(randomAngle);
            if (player.itemAnimation % 4 == 0)
            {
                Main.PlaySound(2, position, 7);
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Cactustoken", 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

