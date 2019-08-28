using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TitaniumChainedchainsaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Titanium Chained-Chainsaw");
			Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 4");
		}
		public override void SetDefaults()
		{
			item.damage = 33;
			item.thrown = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.width = 28;
			item.height = 56;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = 12000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("TitaniumChainedchainsaw");
			item.shootSpeed = 16f;
		item.maxStack = 4;
		}
		public override bool CanUseItem(Player player)
        {
      if ((player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("TitaniumChainedchainsaw2")]) >= item.stack) 
			             {
                    return false;
                }
            else return true;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TitaniumBar, 2);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

