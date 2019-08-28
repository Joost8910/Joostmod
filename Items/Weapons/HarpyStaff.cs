using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HarpyStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Harpy Rod");
			Tooltip.SetDefault("Summons a miniature Harpy to fight for you");
		}
		public override void SetDefaults()
		{
			item.damage = 14;
			item.summon = true;
			item.mana = 10;
			item.width = 34;
			item.height = 34;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 4;
			item.value = 25000;
			item.rare = 2;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("HarpyMinion");
			item.shootSpeed = 7f;
			item.buffType = mod.BuffType("HarpyMinion");
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
			recipe.AddIngredient(ItemID.ShadowScale);
			recipe.AddIngredient(ItemID.Feather, 12);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TissueSample);
			recipe.AddIngredient(ItemID.Feather, 12);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


