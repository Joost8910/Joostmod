using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class ForestWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wand of the Forest");
			Tooltip.SetDefault("Summons a shield of swirling leaves\n" + "Right click to send the leaves outwards");
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.summon = true;
			item.mana = 3;
			item.width = 30;
			item.height = 32;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 4;
			item.noMelee = true; 
			item.knockBack = 4;
			item.value = 5000;
			item.rare = 2;
			item.autoReuse = true;
			item.UseSound = SoundID.Item78;
			item.shoot = mod.ProjectileType("Leaf");
			item.shootSpeed = 7f;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if(player.altFunctionUse == 2)
			{
				for (int l = 0; l < 200; l++)
				{
					Projectile p = Main.projectile[l];
					if (p.type == type && p.active && p.owner == player.whoAmI)
					{
						p.ai[1] = 1f;
					}
				}
			}
			if(player.altFunctionUse != 2)
			{
 				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 90f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 180f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 270f, 0f);
			}
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LivingLeafWall, 8);
			recipe.AddRecipeGroup("Wood", 12);
			recipe.AddTile(TileID.WorkBenches); 
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}


