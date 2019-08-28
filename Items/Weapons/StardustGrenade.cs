using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class StardustGrenade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stardust Grenade");
			Tooltip.SetDefault("Creates stardust cells that chase down enemies upon impact");
		}
		public override void SetDefaults()
		{
			item.damage = 60;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 20;
			item.height = 24;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 5;
			item.value = 2000;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("StardustGrenade");
			item.shootSpeed = 11f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentStardust, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 111);
			recipe.AddRecipe();
		}

	}
}

