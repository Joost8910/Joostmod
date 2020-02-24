using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TrueGungnir : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Gungnir");
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 80;
			item.height = 80;
			item.useTime = 27;
			item.useAnimation = 27;
			item.scale = 1.1f;
			item.knockBack = 7;
			item.value = 500000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useStyle = 5;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("TrueGungnir");
			item.shootSpeed = 7f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] < 1;
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			//Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("TrueGungnirBeam"), damage, knockBack, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gungnir);
			recipe.AddIngredient(null, "BrokenHeroSpear");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


