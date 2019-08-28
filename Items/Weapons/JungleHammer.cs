using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class JungleHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jungle Hammer");
		}
		public override void SetDefaults()
		{
			item.damage = 21;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 24;
			item.useAnimation = 24;
			item.knockBack = 7;
			item.value = 5400;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.hammer = 60;
			item.useStyle = 1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleSpores, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


