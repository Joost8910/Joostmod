using Terraria.ID;
using Terraria.ModLoader;


namespace JoostMod.Items.Weapons
{
	public class EndlessNapalm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Napalm Pouch");
			Tooltip.SetDefault("'Fiery'");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.ranged = true;
			item.damage = 10;
			item.width = 26;
			item.height = 32;
			item.consumable = false;
			item.knockBack = 1f;
			item.value = 480000;
			item.rare = 4;
			item.shoot = mod.ProjectileType("Napalm");
			item.ammo = mod.ItemType("Napalm");
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Napalm", 3996);
			recipe.AddTile(TileID.CrystalBall); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

