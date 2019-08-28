using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class PumpkinGlove : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pumpkin Glove");
			Tooltip.SetDefault("Throws a pumpkin that explodes into homing pumpkins");
		}
		public override void SetDefaults()
		{
			item.damage = 48;
			item.thrown = true;
			item.width = 28;
			item.height = 30;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 1;
			item.knockBack = 8;
			item.value = 120000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Pumpkin2");
			item.shootSpeed = 8f;
		}
	}
}


