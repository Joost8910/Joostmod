using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class ChanneledNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Channeled Nail");
			Tooltip.SetDefault("Hold attack to charge a great slash!\n" + 
			"Unleash it forward while dashing for a long ranged dash slash!");
		}
		public override void SetDefaults()
		{
			item.damage = 22;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.noMelee = true;
			item.useTime = 13;
			item.useAnimation = 13;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 5;
			item.value = 50000;
			item.rare = 2;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("ChanneledNail");
			item.shootSpeed = 10f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("ChanneledNail2")] + player.ownedProjectileCounts[mod.ProjectileType("GreatSlash")] + player.ownedProjectileCounts[mod.ProjectileType("DashSlash")] < 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "SharpenedNail");
			recipe.AddIngredient(ItemID.DemoniteBar, 10);
			recipe.AddIngredient(ItemID.Bone, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "SharpenedNail");
			recipe.AddIngredient(ItemID.CrimtaneBar, 10);
			recipe.AddIngredient(ItemID.Bone, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

