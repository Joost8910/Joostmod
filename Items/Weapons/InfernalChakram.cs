using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;


namespace JoostMod.Items.Weapons
{
	public class InfernalChakram : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infernal Chakram");
			Tooltip.SetDefault("A flaming chakram that creates orbiting fireballs");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
		}
		public override void SetDefaults()
		{
			Item.damage = 36;
			Item.DamageType = DamageClass.Throwing;
			Item.width = 38;
			Item.height = 38;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 225000;
			Item.rare = ItemRarityID.LightRed;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("InfernalChakram").Type;
			Item.shootSpeed = 10f;
		}
		public override bool CanUseItem(Player player)
		{
			return (player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DousedChakram").Type] < 1);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.FireEssence>(50)
				.AddIngredient(ItemID.ThornChakram)
				.AddRecipeGroup("JoostMod:AnyCobalt", 3)
				.AddRecipeGroup("JoostMod:AnyMythril", 3)
				.AddRecipeGroup("JoostMod:AnyAdamantite", 3)
				.AddTile<Tiles.ElementalForge>()
				.Register();
		}
	}
}

