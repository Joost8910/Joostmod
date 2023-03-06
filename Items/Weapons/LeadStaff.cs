using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class LeadStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Staff");
		}
		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Magic;
			Item.width = 42;
			Item.height = 40;
			Item.noMelee = true;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.autoReuse = true;
			Item.mana = 5;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.knockBack = 4;
			Item.value = 5000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item43;
			Item.shoot = ProjectileID.Starfury;
			Item.shootSpeed = 12f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LeadBar, 10)
				.AddIngredient(ItemID.FallenStar, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}

