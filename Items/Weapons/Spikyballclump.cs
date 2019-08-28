using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Spikyballclump : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Clump of Spiky Balls");
			Tooltip.SetDefault("'Your pockets hurt'");
		}
		public override void SetDefaults()
		{
			item.damage = 30;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 20;
			item.height = 20;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 0;
			item.value = 150;
			item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Spikyballclump");
			item.shootSpeed = 11f;
		}


	}
}

