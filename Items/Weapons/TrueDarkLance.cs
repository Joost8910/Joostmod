using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TrueDarkLance : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Dark Lance");
		}
		public override void SetDefaults()
		{
			item.damage = 66;
			item.melee = true;
			item.width = 80;
			item.height = 80;
			item.useTime = 40;
			item.useAnimation = 40;
			item.scale = 1.1f;
			item.knockBack = 9;
			item.value = 100000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useStyle = 5;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("TrueDarkLance");
			item.shootSpeed = 5f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] < 1;
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			//Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TrueDarkLanceBeam"), damage, knockBack, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DarkLance);
			recipe.AddIngredient(null, "BrokenHeroSpear");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


