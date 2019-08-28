using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class BoneHurtingJuice : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bone Hurting Juice");
			Tooltip.SetDefault("Debuff damage steadily increases over time\n" +
                "Debuff deals double damage against skeletal creatures\n" +
                "'Ow, oof, ouch, my bones'");
		}
		public override void SetDefaults()
		{
			item.damage = 18;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 20;
			item.height = 24;
			item.useTime = 27;
			item.useAnimation = 27;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 3;
			item.value = 50;
			item.rare = 1;
			item.UseSound = SoundID.Item106;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("BoneHurtingJuiceBottle");
			item.shootSpeed = 8f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater, 25);
            recipe.AddIngredient(ItemID.Bone);
            recipe.AddIngredient(ItemID.Deathweed);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this, 25);
			recipe.AddRecipe();
		}

	}
}

