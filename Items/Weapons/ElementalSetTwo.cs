using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons
{
	public class ElementalSetTwo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesser Elemental Weapon Set");
			Tooltip.SetDefault("'Unleash the elements!'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 16));
		}
		public override void SetDefaults()
		{
			item.damage = 47;
			item.thrown = true;
			item.width = 22;
			item.height = 30;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 5;
			item.value = 350000;
			item.rare = 5;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("WaterBalloon");
			item.shootSpeed = 10f;

		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int wep = Main.rand.Next(4);
			if (wep == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX * 0.8f), (speedY * 0.8f), mod.ProjectileType("Fireball"), (int)(damage * 0.8f), (knockBack * 0.2f), player.whoAmI);
			}
			if (wep == 1)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Tornade"), (int)(damage * 0.9f), knockBack, player.whoAmI);
			}
			if (wep == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX * 1.2f), (speedY * 1.2f), mod.ProjectileType("WaterBalloon"), (int)(damage * 1f), knockBack, player.whoAmI);
			}
			if (wep == 3)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX), (speedY), mod.ProjectileType("Rock"), (int)(damage * 3.5f), (knockBack * 2), player.whoAmI);
			}
			return false;
		}
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Rock", 999);
			recipe.AddIngredient(null, "Fireball", 999);
			recipe.AddIngredient(null, "Tornade", 999);
			recipe.AddIngredient(null, "WaterBalloon", 999);
			recipe.AddTile(null, "ElementalForge"); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}


