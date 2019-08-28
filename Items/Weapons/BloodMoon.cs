using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class BloodMoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Moon");
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.noMelee = true;
			item.scale = 1.1f;
			item.noUseGraphic = true;
			item.width = 30;
			item.height = 32;
			item.useTime = 44;
			item.useAnimation = 44;
			item.useStyle = 5;
			item.knockBack = 9;
			item.value = 54000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.channel = true;
            item.useTurn = true;
			item.shoot = mod.ProjectileType("BloodMoon");
			item.shootSpeed = 15f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Sunfury);
			recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(null, "TheRose");
			recipe.AddIngredient(ItemID.TheMeatball);
			recipe.AddTile(TileID.DemonAltar); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}
