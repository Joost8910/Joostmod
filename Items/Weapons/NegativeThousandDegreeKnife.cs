using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class NegativeThousandDegreeKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("-1000'C Degrees Knife");
			Tooltip.SetDefault("'That's physically impossible, it's below absolute zero!'\n" + 
			"'Theoretically, AT absolute zero, its atoms wouldn't be able to move at ALL");
		}
		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 20;
			item.height = 18;	
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.knockBack = 1;
			item.value = 500000;
			item.rare = 10;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType("NegativeThousandDegreeKnife");
			item.shootSpeed = 0f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 8); 
			recipe.AddIngredient(ItemID.FrostCore);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

