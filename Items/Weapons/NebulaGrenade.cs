using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class NebulaGrenade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nebula Grenade");
			Tooltip.SetDefault("Creates a nebula upon impact");
		}
		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Throwing;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 20;
			Item.height = 26;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 5;
			Item.value = 2000;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("NebulaGrenade").Type;
			Item.shootSpeed = 11f;
		}
		public override void AddRecipes()
		{
			CreateRecipe(111)
				.AddIngredient(ItemID.FragmentNebula, 1)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

	}
}

