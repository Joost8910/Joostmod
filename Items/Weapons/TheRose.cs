using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TheRose : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Rose");
		}
		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.scale = 1.1f;
			Item.noUseGraphic = true;
			Item.width = 30;
			Item.height = 32;
			Item.useTime = 44;
			Item.useAnimation = 44;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6;
			Item.value = 20000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.channel = true;
			Item.useTurn = true;
			Item.shoot = Mod.Find<ModProjectile>("TheRose").Type;
			Item.shootSpeed = 14f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.JungleSpores, 8)
				.AddIngredient(ItemID.JungleRose)
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}

