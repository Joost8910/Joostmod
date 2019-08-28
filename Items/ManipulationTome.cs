using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class ManipulationTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Manipulation");
            Tooltip.SetDefault("Allows you to pick up and move friendly NPCs\n" + "Right click while holding the NPC to rapidly damage the NPC");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.noMelee = true;
            item.useTime = 5;
            item.useAnimation = 5;
            item.reuseDelay = 5;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 10000;
            item.rare = 2;
            item.shoot = mod.ProjectileType("Manipulation");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            damage = 25;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddIngredient(ItemID.GoldBar);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddIngredient(ItemID.PlatinumBar);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}

