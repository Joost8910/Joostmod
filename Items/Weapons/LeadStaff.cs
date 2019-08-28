using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class LeadStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Staff");
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.magic = true;
			item.width = 42;
			item.height = 40;
			item.noMelee = true;
			item.useTime = 35;
			item.useAnimation = 35;
			item.autoReuse = true;
			item.mana = 5;
			item.useStyle = 4;
			item.knockBack = 4;
			item.value = 5000;
			item.rare = 2;
			item.UseSound = SoundID.Item43;
			item.shoot = 9;
			item.shootSpeed = 12f;
		}
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadBar, 10);
			recipe.AddIngredient(ItemID.FallenStar, 8);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

