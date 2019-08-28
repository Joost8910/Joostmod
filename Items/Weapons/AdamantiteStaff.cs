using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class AdamantiteStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Staff");
            Tooltip.SetDefault("Fires a bouncing laser");
        }
		public override void SetDefaults()
		{
			item.damage = 32;
			item.magic = true;
			item.width = 52;
			item.height = 52;
			item.mana = 6;
			item.useTime = 10;
			item.useAnimation = 10;
			Item.staff[item.type] = true; 
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 0;
			item.value = 75000;
			item.rare = 5;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("GreenLaser");
			item.shootSpeed = 15f;
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AdamantiteBar, 12);
			recipe.AddIngredient(ItemID.Emerald, 1);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}


