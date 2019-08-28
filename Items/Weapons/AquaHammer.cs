using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class AquaHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aqua Hammer");
		}
		public override void SetDefaults()
		{
			item.damage = 16;
			item.melee = true;
			item.width = 42;
			item.height = 46;
			item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 3;
			item.value = 5400;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.hammer = 65;
			item.useStyle = 1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 30);
			recipe.AddIngredient(ItemID.WaterCandle, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


