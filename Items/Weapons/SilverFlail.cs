using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SilverFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Silver Flail");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true;
			item.noMelee = true;
			item.scale = 1.1f;
			item.noUseGraphic = true;
			item.width = 30;
			item.height = 32;
			item.useTime = 44;
			item.useAnimation = 44;
			item.useStyle = 5;
			item.knockBack = 5;
			item.value = 1000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.channel = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("SilverFlail");
			item.shootSpeed = 10f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 8);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

