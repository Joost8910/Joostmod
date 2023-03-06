using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Ammo
{
	public class EndlessDrillBullet : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Drill Bullet Pouch");
            Tooltip.SetDefault("Breaks struck tiles\n" +
                "Does little but rapid damage\n" + 
                "50% Pickaxe Power");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 8;
            Item.width = 26;
            Item.height = 32;
            Item.consumable = false;
            Item.knockBack = 0;
            Item.value = 40000;
            Item.rare = ItemRarityID.Green;
            Item.shoot = Mod.Find<ModProjectile>("DrillBullet").Type;
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Bullet;
        }
		public override void AddRecipes()
		{
			CreateRecipe()
                .AddIngredient<DrillBullet>(3996)
                .AddTile(TileID.CrystalBall)
                .Register();
		}
	}
}

