using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CobaltChainedchainsaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cobalt Chained-Chainsaw");
			Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 4");
		}
		public override void SetDefaults()
		{
			item.damage = 23;
			item.thrown = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.width = 14;
			item.height = 54;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 10000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("CobaltChainedchainsaw");
			item.shootSpeed = 14f;
		item.maxStack = 4;
		}
		public override bool CanUseItem(Player player)
        {
   if ((player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("CobaltChainedchainsaw2")]) >= item.stack) 
	             {
                    return false;
                }
            else return true;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltBar, 2);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

