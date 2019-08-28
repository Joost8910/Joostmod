using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CactusStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Staff");
			Tooltip.SetDefault("Summons a Cactuar to fight for you");
		}
		public override void SetDefaults()
		{
			item.damage = 200;
			item.summon = true;
			item.mana = 10;
			item.width = 58;
			item.height = 58;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 7;
			item.value = 10000000;
			item.rare = 11;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("Cactuar");
			item.shootSpeed = 10f;
			item.buffType = mod.BuffType("Cactuar");	//The buff added to player after used the item
			item.buffTime = 3600;				//The duration of the buff, here is 60 seconds
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
			recipe.AddIngredient(null, "Cactustoken", 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
