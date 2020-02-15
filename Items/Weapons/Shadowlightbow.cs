using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Shadowlightbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadowlight Bow");
			Tooltip.SetDefault("Does not consume ammo\n" + "'Find your inner pieces'");
		}
		public override void SetDefaults()
		{
			item.damage = 40;
			item.ranged = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.knockBack = 5;
			item.value = 144000;
			item.rare = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Darklightarrow");
			item.shootSpeed = 16f;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= player.arrowDamage * (player.archery ? 1.2f : 1f);
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

