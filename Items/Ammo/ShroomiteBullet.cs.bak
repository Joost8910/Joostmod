using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
            Item.maxStack = 999;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 16;
            Item.width = 12;
            Item.height = 12;
            Item.consumable = true;
            Item.knockBack = 5f;
            Item.value = 750;
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.ShroomBullet>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(70)
                .AddIngredient(ItemID.ShroomiteBar)
                .AddIngredient(ItemID.MusketBall, 70)
                .AddTile(TileID.Autohammer)
                .Register();
        }
    }
}

