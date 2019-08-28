using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class IronHatchet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Hatchet");
			Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 3");
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.thrown = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.width = 26;
			item.height = 22;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 1000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("IronHatchet");
			item.shootSpeed = 12f;
		item.maxStack = 3;
		}
		public override bool CanUseItem(Player player)
        {
      if ((player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("IronHatchet2")]) >= item.stack) 
			             {
                    return false;
                }
            else return true;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 2);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

