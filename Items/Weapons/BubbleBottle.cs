using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class BubbleBottle : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubble Knife");
			Tooltip.SetDefault("'Bubbles!'");
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 10;
			item.height = 24;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 2;
			item.value = 5000;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("BubbleBottle");
			item.shootSpeed = 9f;
		}

	}
}

