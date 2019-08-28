using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class GnomeStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome Staff");
			Tooltip.SetDefault("'With silver beard and crimson hat the gnome warriors fight valiantly for their people'\n" + "Summons a Gnome warrior");
		}
		public override void SetDefaults()
		{
			item.damage = 30;
			item.summon = true;
			item.mana = 10;
			item.width = 52;
			item.height = 50;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 3;
			item.value = 64000;
			item.rare = 5;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("Gnome");
			item.shootSpeed = 7f;
			item.buffType = mod.BuffType("Gnome");
			item.buffTime = 3600;
		}
				public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return player.altFunctionUse != 2;
		}
		
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GingerBeard);
			recipe.AddIngredient(ItemID.SilverDye, 2);
			recipe.AddIngredient(ItemID.AdamantiteBar, 8);
			recipe.AddIngredient(ItemID.SoulofMight, 10);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GingerBeard);
			recipe.AddIngredient(ItemID.SilverDye, 2);
			recipe.AddIngredient(ItemID.TitaniumBar, 8);
			recipe.AddIngredient(ItemID.SoulofMight, 10);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}


