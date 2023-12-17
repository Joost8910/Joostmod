using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
	public class EndlessLaserDrillBullet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Laser Drill Bullet Pouch");
            Tooltip.SetDefault("Breaks struck tiles\n" +
                "Does little but rapid damage\n" +
                "Always shoots straight towards your cursor\n" +
                "230% Pickaxe Power");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 15;
            Item.width = 26;
            Item.height = 32;
            Item.consumable = false;
            Item.knockBack = 0;
            Item.value = 160000;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.LaserDrillBullet>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<LaserDrillBullet>(3996)
                .AddTile(TileID.CrystalBall)
                .Register();
            CreateRecipe()
                .AddIngredient<EndlessHallowedDrillBullet>()
                .AddIngredient(ItemID.MartianConduitPlating, 80)
                .AddTile(TileID.CrystalBall)
                .Register();
        }
	}
}

