using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class ShroomiteBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Bullet");
            Tooltip.SetDefault("Leaves a trail of damaging mushrooms");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.ranged = true;
            item.damage = 16;
            item.width = 12;
            item.height = 12;
            item.consumable = true;
            item.knockBack = 5f;
            item.value = 750;
            item.rare = 6;
            item.shoot = mod.ProjectileType("ShroomBullet");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShroomiteBar);
            recipe.AddIngredient(ItemID.MusketBall, 70);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this, 70);
            recipe.AddRecipe();
        }
    }
}

