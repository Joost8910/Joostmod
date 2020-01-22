using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TerraSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Spear");
		}
		public override void SetDefaults()
		{
			item.damage = 75;
			item.melee = true;
			item.width = 100;
			item.height = 100;
			item.useTime = 25;
			item.useAnimation = 25;
			item.scale = 1.1f;
			item.knockBack = 7;
			item.value = 1000000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useStyle = 5;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("TerraSpear");
			item.shootSpeed = 8f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] < 1;
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Terraria.Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			//Terraria.Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TerraSpearBeam"), damage, knockBack, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TrueDarkLance", 1);
			recipe.AddIngredient(null, "TrueGungnir", 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


