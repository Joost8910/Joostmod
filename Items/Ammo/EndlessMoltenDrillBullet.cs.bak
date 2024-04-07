using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
	public class EndlessMoltenDrillBullet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Molten Drill Bullet Pouch");
            Tooltip.SetDefault("Breaks struck tiles\n" +
                "Does little but rapid damage\n" + 
                "100% Pickaxe Power");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 10;
            Item.width = 26;
            Item.height = 32;
            Item.consumable = false;
            Item.knockBack = 0;
            Item.value = 80000;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.MoltenDrillBullet>();
            Item.shootSpeed = 3.5f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MoltenDrillBullet>(3996)
                .AddTile(TileID.CrystalBall)
                .Register();
            CreateRecipe()
                .AddIngredient<EndlessDrillBullet>()
                .AddIngredient(ItemID.HellstoneBar, 40)
                .AddTile(TileID.CrystalBall)
                .Register();
        }
	}
}

