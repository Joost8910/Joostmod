using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class BFE5000 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The BFE 5000");
			Tooltip.SetDefault("'I want it huge, unstable, prone to overheating and a good chance it will blow up if you look at it from the wrong angle.'\n" + 
			"'I didn't become a kerbonaut for an easy ride, dammit!'");
		}
		public override void SetDefaults()
		{
			item.damage = 136;
			item.ranged = true;
			item.width = 12;
			item.height = 88;
			item.noMelee = true;
			item.useTime = 85;
			item.useAnimation = 85;
			item.useStyle = 5;
			item.knockBack = 15;
			item.value = 100000;
			item.rare = 5;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("BFE5000");
			item.shootSpeed = 12f;
		}

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= player.rocketDamage;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position += Vector2.Normalize(new Vector2(speedX, speedY))*80;
            return true;
        }
        public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "KerbalKannon");
			recipe.AddIngredient(ItemID.HallowedBar, 12);
			recipe.AddIngredient(ItemID.SoulofMight, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}

