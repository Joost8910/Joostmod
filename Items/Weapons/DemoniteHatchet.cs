using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class DemoniteHatchet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demonite Hatchet");
			Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 3");
		}
		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.width = 26;
			Item.height = 22;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DemoniteHatchet").Type;
			Item.shootSpeed = 12f;
			Item.maxStack = 3;
		}
		public override bool CanUseItem(Player player)
		{
			if ((player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DemoniteHatchet2").Type]) >= Item.stack)
			{
				return false;
			}
			else return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar, 2)
				.AddIngredient(ItemID.Chain)
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}
