using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class NapalmLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Napalm Launcher");
			Tooltip.SetDefault("Uses napalms for ammo\n" + 
                "25% chance to not consume ammo");
		}
		public override void SetDefaults()
		{
			item.damage = 30;
			item.ranged = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 1;
			item.value = 225000;
			item.rare = 5;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Napalm");
			item.shootSpeed = 8f;
			item.useAmmo = mod.ItemType("Napalm");
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }
        public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() > .25f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FireEssence", 50);
			recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
			recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
			recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
			recipe.AddTile(null, "ElementalForge"); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= player.rocketDamage;
        }
    }
}


