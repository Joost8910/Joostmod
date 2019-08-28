using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CoiledNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coiled Nail");
			Tooltip.SetDefault("Hold attack to charge a great slash!\n" + 
			"Unleash it forward while dashing for a long ranged dash slash!\n" + 
			"Unleash it while holding up or down for a spin attack!");
		}
		public override void SetDefaults()
		{
			item.damage = 44;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.noMelee = true;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 6;
			item.value = 100000;
			item.rare = 4;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("CoiledNail");
			item.shootSpeed = 12f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("CoiledNail2")] + player.ownedProjectileCounts[mod.ProjectileType("GreatSlash")] + player.ownedProjectileCounts[mod.ProjectileType("DashSlash")] + player.ownedProjectileCounts[mod.ProjectileType("SpinSlash")] < 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ChanneledNail");
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

