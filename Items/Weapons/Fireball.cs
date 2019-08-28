using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons
{
	public class Fireball : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");
			Tooltip.SetDefault("Explodes into lingering flames");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
		}
		public override void SetDefaults()
		{
			item.damage = 38;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 16;
			item.height = 22;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 1;
			item.value = 500;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Fireball");
			item.shootSpeed = 7.5f;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 10);
			recipe.AddIngredient(null, "FireEssence");
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}

	}
}

