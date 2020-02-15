using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Weapons
{
	public class DualityStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Staff of Duality");
			Tooltip.SetDefault("'Find your inner pieces'");
		}
		public override void SetDefaults()
		{
			item.damage = 28;
			item.mana = 4;
			item.width = 48;
			item.height = 48;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item8;
			item.useStyle = 5;
			//Item.staff[item.type] = true; 
			item.knockBack = 0;
			item.value = 144000;
			item.rare = 5;
			item.noMelee = true;
			item.magic = true;
			item.channel = true;                            
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LightLaser");
			item.shootSpeed = 12f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, -speedX, -speedY, mod.ProjectileType("DarkLaser"), damage, knockBack, player.whoAmI);
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-24, 0);
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LightShard, 1);
			recipe.AddIngredient(ItemID.DarkShard, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 7);
			recipe.AddIngredient(ItemID.SoulofNight, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

