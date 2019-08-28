using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class MoreNeedles : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("1000 Needles");
            Tooltip.SetDefault("Unleashes a storm of needles");
        }
        public override void SetDefaults()
        {
            item.damage = 1;
            item.magic = true;
            item.mana = 30;
            item.width = 28;
            item.height = 30;
            item.useTime = 5;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.value = 10000;
            item.rare = 7;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Needle2");
            item.shootSpeed = 16f;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            damage = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Needles", 1);
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(ItemID.StyngerBolt, 10);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddIngredient(mod, "Needle", 1000);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 15f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            /*double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 5f;
            double offsetAngle;
            int i;
            for (i = 0; i < 5; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
            }*/

            for (int i = 0; i < 5; i++)
            {
                double baseAngle = Math.Atan2(speedX, speedY);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(randomAngle), baseSpeed * (float)Math.Cos(randomAngle), type, damage, knockBack, player.whoAmI);
            }

            Main.PlaySound(2, position, 7);
            return false;
        }
    }
}

