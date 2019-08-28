using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class EarthenHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Hammer");
			Tooltip.SetDefault("A mighty hammer that creates a shockwave that deals double damage");
		}
		public override void SetDefaults()
		{
			item.damage = 34;
			item.thrown = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 13;
			item.value = 50000;
			item.rare = 4;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("EarthenHammer");
			item.shootSpeed = 8f;
		}
		public override bool CanUseItem(Player player)
        {
 			if (player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave1")] + player.ownedProjectileCounts[mod.ProjectileType("EarthWave2")] >= 1) 
	        {
                  return false;
        	}
            else return true;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "EarthEssence", 50);
			recipe.AddIngredient(ItemID.StoneBlock, 150);
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

