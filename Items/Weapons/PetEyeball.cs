using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PetEyeball : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pet Eyeball");
		}
		public override void SetDefaults()
		{
			item.damage = 76;
			item.melee = true;
			item.width = 42;
			item.height = 50;
			item.useTime = 15;
			item.useAnimation = 15;
			item.reuseDelay = 5;
			item.useStyle = 5;
			item.knockBack = 5;
			item.value = 100000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("PetEyeball");
			item.shootSpeed = 18f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Lens, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ItemID.Chain, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

