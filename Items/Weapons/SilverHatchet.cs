using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SilverHatchet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Silver Hatchet");
			Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 3");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.thrown = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.width = 26;
			item.height = 22;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = 1000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SilverHatchet");
			item.shootSpeed = 12f;
		item.maxStack = 3;
		}
		public override bool CanUseItem(Player player)
        {
       if ((player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("SilverHatchet2")]) >= item.stack) 
			            {
                    return false;
                }
            else return true;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 2);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

