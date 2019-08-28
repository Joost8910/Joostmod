using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class Needles : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("100 Needles");
            Tooltip.SetDefault("Unleashes a flurry of needles");
        }
        public override void SetDefaults()
        {
            item.damage = 1;
            item.magic = true;
            item.mana = 15;
            item.width = 28;
            item.height = 30;
            item.useTime = 3;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 0.5f;
            item.value = 1000;
            item.rare = 3;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Needle5");
            item.shootSpeed = 16f;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            damage = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Needle", 100);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(ItemID.Stinger, 10);
            recipe.AddTile(TileID.WorkBenches);
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
