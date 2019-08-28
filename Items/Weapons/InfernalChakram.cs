using Microsoft.Xna.Framework;
using System.Collections.Generic;
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
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
		}
		public override void SetDefaults()
		{
			item.damage = 41;
			item.thrown = true;
			item.width = 38;
			item.height = 38;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 50000;
			item.rare = 4;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("InfernalChakram");
			item.shootSpeed = 10f;
		}
		public override bool CanUseItem(Player player)
        {
			return (player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("DousedChakram")] < 1) ;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FireEssence", 50);
			recipe.AddIngredient(ItemID.ThornChakram);
			recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

