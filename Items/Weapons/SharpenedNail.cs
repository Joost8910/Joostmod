using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SharpenedNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sharpened Nail");
			Tooltip.SetDefault("Hold attack to charge a great slash!");
		}
		public override void SetDefaults()
		{
			item.damage = 14;
			item.melee = true;
			item.width = 34;
			item.height = 34;
			item.noMelee = true;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 4;
			item.value = 25000;
			item.rare = 1;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("SharpenedNail");
			item.shootSpeed = 8f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("GreatSlash")] + player.ownedProjectileCounts[mod.ProjectileType("SharpenedNail2")] < 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "OldNail");
			recipe.AddIngredient(ItemID.SilverBar, 10);
			recipe.AddIngredient(ItemID.Marble, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "OldNail");
			recipe.AddIngredient(ItemID.TungstenBar, 10);
			recipe.AddIngredient(ItemID.Marble, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

