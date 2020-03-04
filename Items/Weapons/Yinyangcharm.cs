using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Yinyangcharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yin Yang Charm");
			Tooltip.SetDefault("'Find your inner pieces'");
		}
		public override void SetDefaults()
		{
			item.damage = 33;
			item.summon = true;
			item.mana = 15;
			item.width = 42;
			item.height = 42;
			item.useTime = 20;
			item.useAnimation = 40;
			item.useStyle = 4;
			item.knockBack = 4;
			item.value = 144000;
			item.rare = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DarkBolt");
			item.shootSpeed = 12f;
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Terraria.Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Terraria.Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("LightBolt"), damage, knockBack, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LightShard, 1);
			recipe.AddIngredient(ItemID.DarkShard, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 7);
			recipe.AddIngredient(ItemID.SoulofNight, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

