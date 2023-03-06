using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class VileCactus : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vile Cactus Worm");
			Tooltip.SetDefault("Casts a controllable cactus worm");
		}
		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 14;
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.reuseDelay = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.staff[Item.type] = true;
			Item.channel = true;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = 60000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("VileCactusWorm").Type;
			Item.shootSpeed = 10f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.LusciousCactus>(10)
				.Register();
		}
	}
}


