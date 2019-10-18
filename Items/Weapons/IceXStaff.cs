using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class IceXStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Core-X Staff");
			Tooltip.SetDefault("Summons an SA-X Core-X to fight for you");
		}
		public override void SetDefaults()
		{
			item.damage = 300;
			item.summon = true;
			item.mana = 10;
			item.width = 64;
			item.height = 64;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 9;
			item.value = 10000000;
			item.rare = 11;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("IceXMinion");
			item.shootSpeed = 10f;
			item.buffType = mod.BuffType("IceXMinion");
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
			recipe.AddIngredient(null, "IceCoreX", 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}


