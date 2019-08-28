using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SnowFlake : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snow Flake");
			Tooltip.SetDefault("'Don't get frostbite!");
		}
		public override void SetDefaults()
		{
			item.damage = 60;
			item.thrown = true;
			item.maxStack = 1;
			item.consumable = false;
			item.width = 46;
			item.height = 46;
			item.useTime = 33;
			item.useAnimation = 33;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 5;
			item.value = 200000;
			item.rare = 7;
			item.UseSound = SoundID.Item28;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SnowFlake");
			item.shootSpeed = 9f;
		}
	}
}

