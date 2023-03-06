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
			Item.damage = 21;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.knockBack = 7;
			Item.value = 5400;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.hammer = 60;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.JungleSpores, 6)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}


