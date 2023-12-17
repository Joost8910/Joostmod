using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
    public class EndlessHallowedDrillBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Hallowed Drill Bullet Pouch");
            Tooltip.SetDefault("Breaks struck tiles\n" +
                "Does little but rapid damage\n" +
                "200% Pickaxe Power");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 12;
            Item.width = 26;
            Item.height = 32;
            Item.consumable = false;
            Item.knockBack = 0;
            Item.value = 120000;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.HallowedDrillBullet>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<HallowedDrillBullet>(3996)
                .AddTile(TileID.CrystalBall)
                .Register();
            CreateRecipe()
                .AddIngredient<EndlessMoltenDrillBullet>()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
}

