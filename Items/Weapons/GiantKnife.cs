using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class GiantKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant's Knife");
			Tooltip.SetDefault("'It's so long!'");
		}
		public override void SetDefaults()
		{
			item.damage = 40;
			item.thrown = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 28;
			item.height = 76;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 7;
			item.value = 200;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("GiantKnife");
			item.shootSpeed = 8f;
		}
	}
}

