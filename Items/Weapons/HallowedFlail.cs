using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HallowedFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Incandescence");
		}
		public override void SetDefaults()
		{
			item.damage = 67;
			item.melee = true;
			item.noMelee = true;
			item.scale = 1.1f;
			item.noUseGraphic = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 44;
			item.useAnimation = 44;
			item.useStyle = 5;
			item.knockBack = 8;
			item.value = 46000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.channel = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("HallowedFlail");
			item.shootSpeed = 18f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 8);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

