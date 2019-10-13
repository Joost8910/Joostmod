using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Jumbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jumbow");
			Tooltip.SetDefault("'It's huge!'");
		}
		public override void SetDefaults()
		{
			item.damage = 555;
			item.ranged = true;
			item.width = 96;
			item.height = 198;
			item.noMelee = true;
			item.useTime = 40;
			item.useAnimation = 40;
            item.reuseDelay = 5;
            item.noUseGraphic = true;
			item.useStyle = 5;
			item.knockBack = 10;
			item.value = 10000000;
			item.rare = 11;
			item.UseSound = SoundID.Item7;
			item.autoReuse = true;
            item.channel = true;
			item.shoot = mod.ProjectileType("Jumbow");
			item.shootSpeed = 16f;
		}
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= player.arrowDamage * (player.archery ? 1.2f : 1f);
        }
        public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Cactustoken", 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

